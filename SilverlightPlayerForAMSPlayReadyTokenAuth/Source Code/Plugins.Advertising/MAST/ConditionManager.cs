using System;
using System.Collections.Generic;
using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Helpers;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.MAST
{
    /// <summary>
    /// Wraps a MAST Condition, applying the appropriate logic
    /// </summary>
    internal class ConditionManager : IDisposable
    {
        /// <summary>
        /// The condition we are managing
        /// </summary>
        public Condition Condition { get; protected set; }

        /// <summary>
        /// Our MAST Interface to player/system, events and properties
        /// </summary>
        public IMastAdapter MastInterface { get; protected set; }

        /// <summary>
        /// Our child conditions - these are treated as boolean 'AND'.  If we evaluate to true, we need to also check each child.
        /// </summary>
        public List<ConditionManager> Children = new List<ConditionManager>();

        public ConditionManager ParentCondition { get; set; }

        /// <summary>
        /// The event we'll fire to our Trigger parent if the event we are monitoring from the MAST Interface fires.
        /// </summary>
        public event EventHandler EventFired;

        /// <summary>
        /// Track this so we can unwire our event
        /// </summary>
        Delegate eventHandler = null;

        public bool IsEndCondition = false;

        public ConditionManager(Condition condition, IMastAdapter mastInterface)
        {
            if (condition == null)
            {
                throw new NullReferenceException("Condition must not be null");
            }
            if (mastInterface == null)
            {
                throw new NullReferenceException("IMastAdapter must not be null.");
            }

            Condition = condition;
            MastInterface = mastInterface;

            if (condition.type == ConditionType.@event)
            {
                eventHandler = ReflectionHelper.AttachEvent(MastInterface, condition.name, this, "OnEventFired");
            }

            //Wire up our child conditions.  Note - only top level conditions can be based on events, or things would get weird.
            foreach (Condition c in Condition.condition)
            {
                if (c.type == ConditionType.@event)
                {
                    throw new Exception("Event classed conditions can not be children");
                }
                ConditionManager cm = new ConditionManager(c, MastInterface) { ParentCondition = this };
                Children.Add(cm);
            }
        }

        public void DoOnEventFired()
        {
            OnEventFired(this, EventArgs.Empty);
        }

        /// <summary>
        /// Fires from the MAST Interface event, if wired up
        /// </summary>
        public void OnEventFired(object sender, EventArgs args)
        {
            if (EventFired != null)
            {
                EventFired(this, args);
            }
        }

        /// <summary>
        /// Called by our parent to evaluate our condition
        /// </summary>
        /// <returns></returns>
        public bool Evaluate()
        {
            if (Condition == null) return false;

            //always return true here- this way we can properly evaluate children that may be property-based when our event fires.
            if (Condition.type == ConditionType.@event) return true;

            //otherwise we know it's a property, get the value, we'll need it.
            object val = ReflectionHelper.GetValue(MastInterface, Condition.name);

            //do a fancy comparison
            if (CompareValue(val, Condition.value))
            {
                //check children - implicit 'AND'
                foreach (ConditionManager cm in Children)
                {
                    if (!cm.Evaluate()) return false;
                }

                //if none of our children failed, then we're still good.
                return true;
            }

            //if were here, compare returned false
            return false;
        }

        #region Type / value Comparisons

        /// <summary>
        /// Compare two values, with the appropriate operator
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private bool CompareValue(object prop, string val)
        {
            string typeName = prop.GetType().Name.ToLower();
            switch (typeName)
            {
                case "string":
                    return CompareString(prop, val);
                case "timespan":
                    return CompareTimeSpan(prop, val);
                case "datetime":
                    return CompareDateTime(prop, val);
                case "boolean":
                    return CompareBool(prop, val);
                default:
                    if (prop.GetType().IsPrimitive)
                    {
                        //it's not bool or string, must be a number, right?
                        return CompareNumber(prop, val);
                    }
                    else
                    {
                        //unknown type
                        throw new Exception(string.Format("The property type '{0}' is unknown. ", prop.GetType().Name));
                    }
            }
        }

        private bool CompareNumber(object prop, string val)
        {
            double p = (Double)prop;
            double v = Convert.ToDouble(val);

            switch (Condition.@operator)
            {
                case Operator.EQ:
                    return (p == v);
                case Operator.NEQ:
                    return (p != v);
                case Operator.GTR:
                    return (p > v);
                case Operator.GEQ:
                    return (p >= v);
                case Operator.LT:
                    return (p < v);
                case Operator.LEQ:
                    return (p <= v);
                case Operator.MOD:
                    return (p % v == 0);
                default:
                    //unsupported operator
                    throw new Exception(string.Format("The operator {0} is not supported for '{1}' property types. ", Condition.@operator, prop.GetType().Name));
            }
        }

        private bool CompareString(object prop, string val)
        {
            int res = string.Compare(Condition.value, val, StringComparison.CurrentCultureIgnoreCase);

            switch (Condition.@operator)
            {
                case Operator.EQ:
                    //TODO - make option for case sensitivity?  for now stick to ignoring case
                    return (res == 0);
                case Operator.NEQ:
                    return (res != 0);
                case Operator.GTR:
                    return (res > 0);
                case Operator.GEQ:
                    return (res >= 0);
                case Operator.LT:
                    return (res < 0);
                case Operator.LEQ:
                    return (res <= 0);
                default:
                    //unsupported operator
                    throw new Exception(string.Format("The operator {0} is not supported for '{1}' property types. ", Condition.@operator, prop.GetType().Name));
            }
        }

        private bool CompareTimeSpan(object prop, string val)
        {
            TimeSpan p = (TimeSpan)prop;
            TimeSpan v = TimeSpan.Parse(val);

            switch (Condition.@operator)
            {
                case Operator.EQ:
                    return (p == v);
                case Operator.NEQ:
                    return (p != v);
                case Operator.GTR:
                    return (p > v);
                case Operator.GEQ:
                    return (p >= v);
                case Operator.LT:
                    return (p < v);
                case Operator.LEQ:
                    return (p <= v);
                case Operator.MOD:
                    return ModTimeSpans(p, v);
                default:
                    //unsupported operator
                    throw new Exception(string.Format("The operator {0} is not supported for '{1}' property types. ", Condition.@operator, prop.GetType().Name));
            }
        }

        private bool CompareDateTime(object prop, string val)
        {
            DateTime p = (DateTime)prop;

            //special behavior for mod - compareing a timespan to a datetime
            if (Condition.@operator == Operator.MOD)
            {
                TimeSpan t = TimeSpan.Parse(val);
                ModTimeSpans(p.TimeOfDay, t);
            }

            DateTime v = DateTime.Parse(val);

            switch (Condition.@operator)
            {
                case Operator.EQ:
                    return (p == v);
                case Operator.NEQ:
                    return (p != v);
                case Operator.GTR:
                    return (p > v);
                case Operator.GEQ:
                    return (p >= v);
                case Operator.LT:
                    return (p < v);
                case Operator.LEQ:
                    return (p <= v);
                default:
                    //unsupported operator
                    throw new Exception(string.Format("The operator {0} is not supported for '{1}' property types. ", Condition.@operator, prop.GetType().Name));
            }
        }

        bool modTimerSet = false;

        private bool ModTimeSpans(TimeSpan prop, TimeSpan val)
        {
            TimeSpan remainder = prop;
            while (remainder >= val) remainder -= val;
            //exact match - but that's a slim chance, so give a little slop
            if (prop == val || prop - val <= TimeSpan.FromMilliseconds(100)) return true;

            if (!modTimerSet && prop > TimeSpan.FromMilliseconds(100))
            {
                //set a one-time timer
                AsyncHelper.SetTimer(DoOnEventFired, "TimeModTimer", ReenteranceMode.Bypass, prop, TimeSpan.FromMilliseconds(-1));
                modTimerSet = true;
            }

            return false;
        }

        private bool CompareBool(object prop, string val)
        {
            switch (Condition.@operator)
            {
                case Operator.EQ:
                    //TODO - make option for case sensitivity?  for now stick to ignoring case
                    return ((bool)prop == ConversionHelper.ParseBool(val));
                case Operator.NEQ:
                    return ((bool)prop != ConversionHelper.ParseBool(val));
                default:
                    //unsupported operator
                    throw new Exception(string.Format("The operator {0} is not supported for '{1}' property types. ", Condition.@operator, prop.GetType().Name));
            }
        }

        #endregion

        public void Dispose()
        {
            //cleanup timer, if needed
            modTimerSet = false;
            AsyncHelper.StopTimer("TimeModTimer");

            if (eventHandler != null)
            {
                ReflectionHelper.DetachEvent(MastInterface, eventHandler);
            }

            foreach (ConditionManager cm in Children)
            {
                cm.Dispose();
            }
        }
    }
}
