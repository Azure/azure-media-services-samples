// <copyright file="TimeTree.cs" company="Microsoft Corporation">
// ===============================================================================
// MICROSOFT CONFIDENTIAL
// Microsoft Accessibility Business Unite
// Incubation Lab
// Project: Timed Text Library
// ===============================================================================
// Copyright 2009  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace TimedText.Timing
{
    /// <summary>
    /// Denotes whether time containment is parallel or sequential
    /// </summary>
    public enum TimeContainer
    {
        Par,
        Seq
    };



    /// <summary>
    /// A Tree of elements, where each element supports a list of annotations.
    /// </summary>
    /// <typeparam name="TChildren">Type of children elements</typeparam>
    /// <typeparam name="TAttribute">Type of attribute annotations</typeparam>
    public class TimeTree<TChildren, TAttribute>
    {

        public class TreeType : TimeTree<TChildren, TAttribute>
        {
        }

        #region Variables and Properties
        private Dictionary<string, object> m_timing = new Dictionary<string, object>();

        /// <summary>
        /// The begin, end and dur times for this node
        /// </summary>
        public Dictionary<string, object> Timing { get { return m_timing; } }

        private Dictionary<string, object> m_metadata = new Dictionary<string, object>();

        /// <summary>
        /// Metadata associated with this node
        /// </summary>
        public Dictionary<string, object> Metadata { get { return m_metadata; } }

        //private TimeTreeCollection m_children;
        private Collection<TreeType> m_children;
        private Collection<TAttribute> m_attributes;

        private TimeContainer m_timeSemantics = TimeContainer.Par;

        /// <summary>
        /// Specifies whether children are sequential or parallel in time.
        /// unless an element overrides, the default is par.
        /// </summary>
        /// <returns></returns>
        public TimeContainer TimeSemantics
        {
            get { return m_timeSemantics; }
            set { m_timeSemantics = value; }
        }

        private TimeCode m_startTime;
        private TimeCode m_endTime;
        private TreeType m_parent;

        /// <summary>
        /// tree node which is the unique parent of this node
        /// </summary>
        public TreeType Parent
        {
            get
            {
                return m_parent;
            }
            set
            {
                m_parent = value;
            }
        }

        /// <summary>
        /// List of time trees that are contained within this node
        /// </summary>
        public Collection<TreeType> Children
        {
            get { return m_children; }
        }

        /// <summary>
        /// List of attributes associated with this node
        /// </summary>
        public Collection<TAttribute> Attributes
        {
            get { return new Collection<TAttribute>(m_attributes); }
        }

        /// <summary>


        /// <summary>
        /// Get the time at which this element becomes active
        /// </summary>
        /// <returns></returns>
        public TimeCode Begin
        {
            get
            {
                return m_startTime;
            }
        }

        /// <summary>
        /// Get the time at which this element is no longer active
        /// </summary>
        /// <returns></returns>
        public TimeCode End
        {
            get
            {
                return m_endTime;
            }
        }

        /// <summary>
        /// Get the time at which this element is no longer active
        /// </summary>
        /// <returns></returns>
        public TimeCode Duration
        {
            get
            {
                return m_endTime - m_startTime;
            }
        }

        #endregion

        #region constructors

        public TimeTree()
        {
            m_children = new Collection<TreeType>();
            m_attributes = new Collection<TAttribute>();
        }

        public TimeTree(Collection<TAttribute> attributes, Collection<TreeType> children)
        {
            m_children = children;
            m_attributes = attributes;
        }

        #endregion

        #region time container semantics
        /// <summary>
        /// Test if the tree is active at the given time
        /// </summary>
        /// <param name="time">time of test</param>
        /// <returns>true if active</returns>
        public bool TemporallyActive(TimeCode time)
        {
            return (Begin <= time && time < End);
        }

        /// <summary>
        /// Apply function to all elements in the tree and return a
        /// flattened list
        /// </summary>
        /// <typeparam name="X"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        //public List<X> Reduce<X>(Func<TimeTree<T, A>, List<X>> Fn)

        private List<X> Reduce<X>(Func<TreeType, List<X>> function)
        {
            List<X> result = new List<X>();
            result.AddRange(function((TreeType)this));
            foreach (var c in m_children)
            {
                result.AddRange(c.Reduce<X>(function));
            }
            return result;
        }

        /// <summary>
        /// return an ordered list of the significant time events 
        /// in the time tree.
        /// </summary>
        public ReadOnlyCollection<TimeCode> Events
        {
            get
            {
                var query = from n in Reduce<TimeCode>(
                                        (TreeType tree) =>
                                        {
                                            List<TimeCode> t = new List<TimeCode>();
                                            t.Add(tree.Begin);
                                            t.Add(tree.End);
                                            return t;
                                        }
                                     )
                            orderby n
                            select n;
                var res = query.ToList<TimeCode>().Distinct<TimeCode>();
                return new ReadOnlyCollection<TimeCode>(res.ToList<TimeCode>());
            }
        }

        /// <summary>
        /// Walk the tree to determine the absolute start and end times of all the elements.
        /// the reference times passed in are absolute times, the result of calling this is to set the local start time
        /// and end time to absolute times between these two reference times, based on the begin, end and dur attributes
        /// and to recursively set all of the children.
        /// </summary>
        public void ComputeTimeIntervals(TimeContainer context, TimeCode referenceStart, TimeCode referenceEnd)
        {
            m_startTime = new TimeCode(0d, TimeExpression.CurrentSmpteFrameRate);
            m_endTime = new TimeCode(0d, TimeExpression.CurrentSmpteFrameRate);

            // compute the beginning of my interval.
            TimeCode begin = (Timing.ContainsKey("begin")) ? (TimeCode)Timing["begin"] : new TimeCode(0, TimeExpression.CurrentSmpteFrameRate);
            m_startTime = referenceStart + begin;

            TimeCode referenceDur, dur, end;
            // compute the simple duration of the interval ,  
            // par children have indefinite default duration, seq children have zero default duration.
            // (we dont support indefinite here but  truncate to the outer container)
            // does end work here?  surely it truncates the active duration, 
            if (!Timing.ContainsKey("dur") && !Timing.ContainsKey("end") && context == TimeContainer.Seq)
            {
                referenceDur = new TimeCode(0, TimeExpression.CurrentSmpteFrameRate);
            }
            else
            {
                if (m_startTime < referenceEnd)
                {
                    referenceDur = referenceEnd - m_startTime;
                }
                else
                {
                    referenceDur = new TimeCode(0, TimeExpression.CurrentSmpteFrameRate);
                }
            }

            bool containsDur = false;
            if (Timing.ContainsKey("dur"))
            {
                containsDur = true;
                dur = (TimeCode)Timing["dur"];
                if (dur > referenceDur)
                {
                    dur = referenceDur;
                }
            }
            else
            {
                dur = referenceDur;
            }

            m_endTime = m_startTime + dur;

            // end can truncate the simple duration.
            TimeCode offsetEnd = new TimeCode(0, TimeExpression.CurrentSmpteFrameRate);
            offsetEnd += referenceStart;

            if (Timing.ContainsKey("end"))
            {
                end = referenceStart + ((TimeCode)Timing["end"]);
            }
            else
            {
                // Original code:
                //
                // end = referenceEnd;
                //
                // NOTE: This logic was changed from original TimedText library to properly handle 
                //       Sequential time containers when the duration is indefinite.
                if (context == TimeContainer.Par)
                {
                    end = referenceEnd;
                }
                else
                {
                    end = m_startTime + referenceDur;
                }
            }
            //end = (Timing.ContainsKey("end")) ? (m_startTime.Add((TimeSpan)Timing["end"])) : referenceEnd;
            if (!containsDur)
            {
                m_endTime = end;
            }
            else
            {
                m_endTime = (end < m_endTime) ? end : m_endTime;
            }

            if (TimeSemantics == TimeContainer.Par)
            {
                foreach (TimeTree<TChildren, TAttribute> child in m_children)
                {
                    child.ComputeTimeIntervals(TimeSemantics, m_startTime, m_endTime);
                }
            }
            else
            {
                TimeCode s = m_startTime;
                foreach (TimeTree<TChildren, TAttribute> child in m_children)
                {
                    child.ComputeTimeIntervals(TimeSemantics, s, m_endTime);
                    s = child.m_endTime;
                }
            }
        }
        #endregion


        #region Testing
        internal static bool UnitTests()
        {
            #region string preamble
            string preamble = @"<tt xml:lang =''
                   ttp:profile='http://www.w3.org/2006/10/ttaf1#profile-dfxp'
                   xmlns='http://www.w3.org/2006/10/ttaf1'
                   xmlns:ttm='http://www.w3.org/2006/10/ttaf1#metadata'
                   xmlns:ttp='http://www.w3.org/2006/10/ttaf1#parameter'
                   xmlns:tts='http://www.w3.org/2006/10/ttaf1#style' >
                 <head>
                       <ttm:title>Unit test</ttm:title>
                </head>
            ";
            #endregion

            XElement ttData1 = XElement.Parse(preamble + @"
                  <body timeContainer ='par' >
                        <div timeContainer ='seq' end='5s'>
                            <p  dur='2s'>One<span begin='0.5s' dur='1s' > and a bit</span></p>
                            <p  dur='2s'>Two</p>
                        </div>
                         <div timeContainer ='seq' dur='5s'>
                            <p  begin='0.1s' dur='2s'>One</p>
                            <p  dur='2s'>Two</p>
                        </div>
               </body>
            </tt>
            ");

            TimedTextElementBase tt1 = TimedTextElementBase.Parse(ttData1);
            bool pass = tt1.Valid();
            TimeCode startTime = new TimeCode("00:00:00:00", TimeExpression.CurrentSmpteFrameRate);
            TimeCode endTime = new TimeCode("01:00:00:00", TimeExpression.CurrentSmpteFrameRate);

            tt1.ComputeTimeIntervals(TimeContainer.Par, startTime, endTime);
            ReadOnlyCollection<TimeCode> ev = tt1.Events;

            // some basic tests, try to come up with some more devilish ones.
            pass &= ev.Count == 10;

            return pass;
        }
        #endregion

        #region Attic
#if false
        public TimeTree<T, A> Walk(Func<TimeTree<T, A>, TimeTree<T, A>> Fn, Func<A, A> An)
        {
            List<A> newattribs = new List<A>();

            foreach (A d in m_attributes)
            {
                newattribs.Add(An(d));
            }

            List<TimeTree<T, A>> newkids = new List<TimeTree<T, A>>();

            foreach (TimeTree<T, A> c in m_children)
            {
                newkids.Add(Fn(c));
            }
            return new TimeTree<T, A>(newattribs, newkids);

        }
#endif
        #endregion
    }
}