// <copyright file="TimeCode.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimeCode.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

using System;
using System.Runtime.InteropServices;
using Microsoft.SilverlightMediaFramework.Plugins.TimedText.Resources;

namespace TimedText.Timing
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using Microsoft.SilverlightMediaFramework.Core.Media;

    /// <summary>
    /// Represents a SMPTE 12M standard time code and provides conversion operations to various SMPTE time code formats and rates.
    /// </summary>
    /// <remarks>
    /// Framerates supported by the TimeCode class include, 23.98 IVTC Film Sync, 24fps Film Sync, 25fps PAL, 29.97 drop frame,
    /// 29.97 Non drop, and 30fps.
    /// </remarks>
    [ComVisible(true)]
#if !SILVERLIGHT
    [Serializable]
#endif
    public partial struct TimeCode : IComparable, IComparable<TimeCode>, IEquatable<TimeCode>
    {
        /// <summary>
        /// Regular expression string used for parsing out the timecode.
        /// </summary>
        private const string SMPTEREGEXSTRING = "(?<Hours>\\d{2}):(?<Minutes>\\d{2}):(?<Seconds>\\d{2})(?::|;)(?<Frames>\\d{2})";

#if !SILVERLIGHT
        private const RegexOptions TimecodeRegexOptions = RegexOptions.CultureInvariant | RegexOptions.Compiled;
#else
        private const RegexOptions TimecodeRegexOptions = RegexOptions.CultureInvariant;
#endif

        /// <summary>
        /// Regular expression object used for validating timecode.
        /// </summary>
        private static readonly Regex _validateTimecode = new Regex(SMPTEREGEXSTRING, TimecodeRegexOptions);

        private AbsoluteTimeHelper absoluteTime;

        /// <summary>
        /// The frame rate for this instance.
        /// </summary>
        private SmpteFrameRate frameRate;

        /// <summary>
        /// Wraps the AbsoluteTime value
        /// </summary>
        internal class AbsoluteTimeHelper
        {
            public AbsoluteTimeHelper(float absoluteTime)
            {
                this.TimeAsFloat = absoluteTime;
                this.TimeAsDouble = (double)absoluteTime;

                this.Time27Mhz = AbsoluteTimeToTicks27Mhz(this);
                this.PcrTime = AbsoluteTimeToTicksPcrTb(this);

            }

            public AbsoluteTimeHelper(double absoluteTime)
            {
                this.TimeAsDouble = (double)absoluteTime;
                this.TimeAsFloat = (float)absoluteTime;

                this.Time27Mhz = AbsoluteTimeToTicks27Mhz(this);
                this.PcrTime = AbsoluteTimeToTicksPcrTb(this);
            }

            public AbsoluteTimeHelper(ulong ticks27Mhz)
            {
                double absoluteTime = Ticks27MhzToAbsoluteTime(ticks27Mhz);
                this.TimeAsDouble = absoluteTime;
                this.TimeAsFloat = (float)absoluteTime;

                this.Time27Mhz = ticks27Mhz;
                this.PcrTime = Ticks27MhzToPcrTb(ticks27Mhz);
            }

            public float TimeAsFloat { get; private set; }

            public double TimeAsDouble { get; private set; }

            public ulong PcrTime { get; private set; }

            public ulong Time27Mhz { get; private set; }
        }

        /// <summary>
        ///  Initializes a new instance of the TimeCode struct to a specified number of hours, minutes, and seconds.
        /// </summary>
        /// <param name="hours">Number of hours.</param>
        /// <param name="minutes">Number of minutes.</param>
        /// <param name="seconds">Number of seconds.</param>
        /// <param name="frames">Number of frames.</param>
        /// <param name="rate">The SMPTE frame rate.</param>
        /// <code source="..\Documentation\SdkDocSamples\TimecodeSamples.cs" region="CreateTimeCode_2398FromIntegers" lang="CSharp" title="Create TimeCode from Integers"/>
        public TimeCode(int hours, int minutes, int seconds, int frames, SmpteFrameRate rate)
        {
            string timeCode = String.Format(CultureInfo.InvariantCulture, "{0:D2}:{1:D2}:{2:D2}:{3:D2}", hours, minutes, seconds, frames);
            this.frameRate = rate;
            this.absoluteTime = Smpte12mToAbsoluteTime(timeCode, this.frameRate);
        }

        /// <summary>
        ///  Initializes a new instance of the TimeCode struct to a specified number of hours, minutes, and seconds.
        /// </summary>
        /// <param name="days">Number of days.</param>
        /// <param name="hours">Number of hours.</param>
        /// <param name="minutes">Number of minutes.</param>
        /// <param name="seconds">Number of seconds.</param>
        /// <param name="frames">Number of frames.</param>
        /// <param name="rate">The SMPTE frame rate.</param>
        /// <code source="..\Documentation\SdkDocSamples\TimecodeSamples.cs" region="CreateTimeCode_2398FromIntegers" lang="CSharp" title="Create TimeCode from Integers"/>
        public TimeCode(int days, int hours, int minutes, int seconds, int frames, SmpteFrameRate rate)
        {
            string timeCode = String.Format(CultureInfo.InvariantCulture, "{0}:{1:D2}:{2:D2}:{3:D2}:{4:D2}", days, hours, minutes, seconds, frames);
            this.frameRate = rate;
            this.absoluteTime = Smpte12mToAbsoluteTime(timeCode, this.frameRate);
        }

        /// <summary>
        /// Initializes a new instance of the TimeCode struct using an Int32 in hex format containing the time code value compatible with the Windows Media Format SDK.
        /// Time code is stored so that the hexadecimal value is read as if it were a decimal value. That is, the time code value 0x01133512 does not represent decimal 18035986, rather it specifies 1 hour, 13 minutes, 35 seconds, and 12 frames.
        /// </summary>
        /// <param name="windowsMediaTimeCode">The integer value of the timecode.</param>
        /// <param name="rate">The SMPTE frame rate.</param>
        public TimeCode(int windowsMediaTimeCode, SmpteFrameRate rate)
        {
            // Timecode is provided back formatted as hexadecimal bytes read in single bytes from left to right.
            byte[] timeCodeBytes = BitConverter.GetBytes(windowsMediaTimeCode);
            string timeCode = String.Format(CultureInfo.InvariantCulture, "{3:x2}:{2:x2}:{1:x2}:{0:x2}", timeCodeBytes[0], timeCodeBytes[1], timeCodeBytes[2], timeCodeBytes[3]);

            this.frameRate = rate;
            this.absoluteTime = Smpte12mToAbsoluteTime(timeCode, this.frameRate);
        }

        /// <summary>
        /// Initializes a new instance of the TimeCode struct using the TotalSeconds in the supplied TimeSpan.
        /// </summary>
        /// <param name="timeSpan">The <see cref="TimeSpan"/> to be used for the new timecode.</param>
        /// <param name="rate">The SMPTE frame rate.</param>
        public TimeCode(TimeSpan timeSpan, SmpteFrameRate rate)
        {
            this.frameRate = rate;
            this.absoluteTime = TimeCode.FromTimeSpan(timeSpan, rate).AbsoluteTime;
        }

        /// <summary>
        /// Initializes a new instance of the TimeCode struct using a time code string that contains the framerate at the end of the string.
        /// </summary>
        /// <remarks>
        /// Pass in a timecode in the format "timecode@framrate". 
        /// Supported rates include @23.98, @24, @25, @29.97, @30
        /// </remarks>
        /// <example>
        /// "00:01:00:00@29.97" is equivalent to 29.97 non drop frame.
        /// "00:01:00;00@29.97" is equivalent to 29.97 drop frame.
        /// </example>
        /// <param name="timeCodeAndRate">The SMPTE 12m time code string.</param>
        public TimeCode(string timeCodeAndRate)
        {
            string[] timeAndRate = timeCodeAndRate.Split('@');

            string time = string.Empty;
            string rate = string.Empty;

            if (timeAndRate.Length == 1)
            {
                time = timeAndRate[0];
                rate = "29.97";
            }
            else if (timeAndRate.Length == 2)
            {
                time = timeAndRate[0];
                rate = timeAndRate[1];
            }

            this.frameRate = SmpteFrameRate.Smpte2997NonDrop;

            if (rate == "29.97" && time.IndexOf(';') > -1)
            {
                this.frameRate = SmpteFrameRate.Smpte2997Drop;
            }
            else if (rate == "29.97" && time.IndexOf(';') == -1)
            {
                this.frameRate = SmpteFrameRate.Smpte2997NonDrop;
            }
            else if (rate == "25")
            {
                this.frameRate = SmpteFrameRate.Smpte25;
            }
            else if (rate == "23.98")
            {
                this.frameRate = SmpteFrameRate.Smpte2398;
            }
            else if (rate == "24")
            {
                this.frameRate = SmpteFrameRate.Smpte24;
            }
            else if (rate == "30")
            {
                this.frameRate = SmpteFrameRate.Smpte30;
            }

            this.absoluteTime = Smpte12mToAbsoluteTime(time, this.frameRate);
        }

        /// <summary>
        /// Initializes a new instance of the TimeCode struct using a time code string and a SMPTE framerate.
        /// </summary>
        /// <param name="timeCode">The SMPTE 12m time code string.</param>
        /// <param name="rate">The SMPTE framerate used for this instance of TimeCode.</param>
        public TimeCode(string timeCode, SmpteFrameRate rate)
        {
            this.frameRate = rate;
            this.absoluteTime = Smpte12mToAbsoluteTime(timeCode, this.frameRate);
        }

        /// <summary>
        /// Initializes a new instance of the TimeCode struct using an absolute time value, and the SMPTE framerate.
        /// </summary>
        /// <param name="absoluteTime">The double that represents the absolute time value.</param>
        /// <param name="rate">The SMPTE framerate that this instance should use.</param>
        public TimeCode(double absoluteTime, SmpteFrameRate rate)
        {
            this.absoluteTime = new AbsoluteTimeHelper(absoluteTime);
            this.frameRate = rate;
        }


        /// <summary>
        /// Initializes a new instance of the TimeCode struct a long value that represents a value of a 27 Mhz clock.
        /// </summary>
        /// <param name="ticks27Mhz">The long value in 27 Mhz clock ticks.</param>
        /// <param name="rate">The SMPTE frame rate to use for this instance.</param>
        public TimeCode(ulong ticks27Mhz, SmpteFrameRate rate)
        {
            this.absoluteTime = new AbsoluteTimeHelper(ticks27Mhz);
            this.frameRate = rate;
        }

        /// <summary>
        ///  Initializes a new instance of the TimeCode struct to a specified number of hours, minutes, and seconds.
        /// </summary>
        /// <param name="hours">Number of hours</param>
        /// <param name="minutes">Number of minutes</param>
        /// <param name="seconds">Number of seconds</param>
        /// <param name="frames">Number of frames</param>
        /// <param name="rate">The Smpte frame rate</param>
        /// <exception cref="System.FormatException">
        /// The parameters specify a TimeCode value less than TimeCode.MinValue
        /// or greater than TimeCode.MaxValue, or the values of time code components are not valid for the 
        /// Smpte framerate.
        /// </exception>
        /// <code source="..\Documentation\SdkDocSamples\TimecodeSamples.cs" region="CreateTimeCode_2398FromIntegers" lang="CSharp" title="Create TimeCode from Integers"/>
        public TimeCode(int hours, int minutes, int seconds, int frames, decimal rate)
        {
            // string timeCode = String.Format(CultureInfo.InvariantCulture, "{0:D2}:{1:D2}:{2:D2}:{3:D2}", hours, minutes, seconds, frames);
            this.frameRate = SmpteFrameRate.Unknown;
            decimal actualRate = rate;
            if (hours > 23) throw new FormatException("hours cannot be greater than 23");
            if (minutes > 59) throw new FormatException("minutes cannot be greater than 59");
            if (seconds > 59) throw new FormatException("seconds cannot be greater than 59");
            if (frames >= rate) throw new FormatException("frames cannot be greater than rate");
            this.absoluteTime = new AbsoluteTimeHelper((double)((1 / actualRate) * (frames + (actualRate * seconds) + (1800 * minutes) + (108000 * hours))));
        }


        /// <summary>
        /// Initializes a new instance of the TimeCode struct using the TotalSeconds in the supplied TimeSpan.
        /// </summary>
        /// <param name="timeSpan">The <see cref="TimeSpan"/> to be used for the new timecode.</param>
        /// <param name="rate">The Smpte frame rate</param>
        public TimeCode(TimeSpan timeSpan, decimal rate)
        {
            this.frameRate = SmpteFrameRate.Unknown;
            this.absoluteTime = new AbsoluteTimeHelper(timeSpan.TotalSeconds);
        }

        /// <summary>
        ///  Gets the number of ticks in 1 day. 
        ///  This field is constant.
        /// </summary>
        /// <value>The number of ticks in 1 day.</value>
        public static ulong TicksPerDay
        {
            get { return 864000000000; }
        }

        /// <summary>
        ///  Gets the number of absolute time ticks in 1 day. 
        ///  This field is constant.
        /// </summary>
        /// <value>The number of absolute time ticks in 1 day.</value>
        public static double TicksPerDayAbsoluteTime
        {
            get { return 86400; }
        }

        /// <summary>
        ///  Gets the number of ticks in 1 hour. This field is constant.
        /// </summary>
        /// <value>The number of ticks in 1 hour.</value>
        public static ulong TicksPerHour
        {
            get { return 36000000000; }
        }

        /// <summary>
        ///  Gets the number of absolute time ticks in 1 hour. This field is constant.
        /// </summary>
        /// <value>The number of absolute time ticks in 1 hour.</value>
        public static double TicksPerHourAbsoluteTime
        {
            get { return 3600; }
        }

        /// <summary>
        /// Gets the number of ticks in 1 millisecond. This field is constant.
        /// </summary>
        /// <value>The number of ticks in 1 millisecond.</value>
        public static ulong TicksPerMillisecond
        {
            get { return 10000; }
        }

        /// <summary>
        /// Gets the number of ticks in 1 millisecond. This field is constant.
        /// </summary>
        /// <value>The number of ticks in 1 millisecond.</value>
        public static double TicksPerMillisecondAbsoluteTime
        {
            get { return 0.0010000D; }
        }

        /// <summary>
        /// Gets the number of ticks in 1 minute. This field is constant.
        /// </summary>
        /// <value>The number of ticks in 1 minute.</value>
        public static ulong TicksPerMinute
        {
            get { return 600000000; }
        }

        /// <summary>
        /// Gets the number of absolute time ticks in 1 minute. This field is constant.
        /// </summary>
        /// <value>The number of absolute time ticks in 1 minute.</value>
        public static double TicksPerMinuteAbsoluteTime
        {
            get { return 60; }
        }

        /// <summary>
        /// Gets the number of ticks in 1 second.
        /// </summary>
        /// <value>The number of ticks in 1 second.</value>
        public static ulong TicksPerSecond
        {
            get { return 10000000; }
        }

        /// <summary>
        /// Gets the number of ticks in 1 second.
        /// </summary>
        /// <value>The number of ticks in 1 second.</value>
        public static double TicksPerSecondAbsoluteTime
        {
            get { return 1.0000000D; }
        }

        /// <summary>
        /// Gets the minimum TimeCode value. This field is read-only.
        /// </summary>
        /// <value>The minimum TimeCode value.</value>
        public static double MinValue
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets the absolute time in seconds of the current TimeCode object.
        /// </summary>
        /// <returns>
        ///  A double that is the absolute time in seconds duration of the current TimeCode object.
        /// </returns>
        /// <value>The absolute time in seconds of the current TimeCode.</value>
        public double Duration
        {
            get { return Convert.ToDouble(this.AbsoluteTime, CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Gets or sets the current SMPTE framerate for this TimeCode instance.
        /// </summary>
        /// <value>The frame rate of the TimeCode.</value>
        public SmpteFrameRate FrameRate
        {
            get { return this.frameRate; }
            set { this.frameRate = value; }
        }

        /// <summary>
        ///  Gets the number of whole hours represented by the current TimeCode
        ///  structure.
        /// </summary>
        /// <returns>
        ///  The hour component of the current TimeCode structure. The return value
        ///     ranges from 0 through 23.
        /// </returns>
        /// <value>The number of whole hours of the TimeCode.</value>
        public int HoursSegment
        {
            get
            {
                string timeCode = AbsoluteTimeToSmpte12M(this.AbsoluteTime, this.frameRate);
                string hours = timeCode.Substring(0, 2);
                return Int32.Parse(hours, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the number of whole minutes represented by the current TimeCode structure.
        /// </summary>
        /// <returns>
        /// The minute component of the current TimeCode structure. The return
        /// value ranges from 0 through 59.
        /// </returns>
        /// <value>The number of whole minutes of the current TimeCode.</value>
        public int MinutesSegment
        {
            get
            {
                string timeCode = AbsoluteTimeToSmpte12M(this.AbsoluteTime, this.frameRate);
                string minutes = timeCode.Substring(3, 2);
                return Int32.Parse(minutes, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the number of whole seconds represented by the current TimeCode structure.
        /// </summary>
        /// <returns>
        ///  The second component of the current TimeCode structure. The return
        ///    value ranges from 0 through 59.
        /// </returns>
        /// <value>The number of whole seconds of the current TimeCode.</value>
        public int SecondsSegment
        {
            get
            {
                string timeCode = AbsoluteTimeToSmpte12M(this.AbsoluteTime, this.frameRate);
                string seconds = timeCode.Substring(6, 2);
                return Int32.Parse(seconds, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the number of whole frames represented by the current TimeCode
        ///     structure.
        /// </summary>
        /// <returns>
        /// The frame component of the current TimeCode structure. The return
        ///     value depends on the framerate selected for this instance. All frame counts start at zero.
        /// </returns>
        /// <value>The number of whole frames of the TimeCode.</value>
        public int FramesSegment
        {
            get
            {
                string timeCode = AbsoluteTimeToSmpte12M(this.AbsoluteTime, this.frameRate);
                string frames = timeCode.Substring(9, 2);
                return Int32.Parse(frames, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the value of the current TimeCode structure expressed in whole
        ///     and fractional days.
        /// </summary>
        /// <returns>
        ///  The total number of days represented by this instance.
        /// </returns>
        /// <value>The number of days of the TimeCode.</value>
        public double TotalDays
        {
            get
            {
                ulong framecount = AbsoluteTimeToFrames(this.AbsoluteTime, this.frameRate);
                return (framecount / 108000D) / 24;
            }
        }

        /// <summary>
        /// Gets the value of the current TimeCode structure expressed in whole
        ///     and fractional hours.
        /// </summary>
        /// <returns>
        ///  The total number of hours represented by this instance.
        /// </returns>
        /// <value>The number of hours of the TimeCode.</value>
        public double TotalHours
        {
            get
            {
                ulong framecount = AbsoluteTimeToFrames(this.AbsoluteTime, this.frameRate);


                double hours;

                switch (this.frameRate)
                {
                    case SmpteFrameRate.Smpte2398:
                    case SmpteFrameRate.Smpte24:
                        hours = framecount / 86400D;
                        break;
                    case SmpteFrameRate.Smpte25:
                        hours = framecount / 90000D;
                        break;
                    case SmpteFrameRate.Smpte2997Drop:
                        hours = framecount / 107892D;
                        break;
                    case SmpteFrameRate.Smpte2997NonDrop:
                    case SmpteFrameRate.Smpte30:
                        hours = framecount / 108000D;
                        break;
                    default:
                        hours = framecount / 108000D;
                        break;
                }

                return hours;
            }
        }

        /// <summary>
        /// Gets the value of the current TimeCode structure expressed in whole
        /// and fractional minutes.
        /// </summary>
        /// <returns>
        ///  The total number of minutes represented by this instance.
        /// </returns>
        /// <value>The number of minutes of the TimeCode.</value>
        public double TotalMinutes
        {
            get
            {
                ulong framecount = AbsoluteTimeToFrames(this.AbsoluteTime, this.frameRate);

                double minutes;

                switch (this.frameRate)
                {
                    case SmpteFrameRate.Smpte2398:
                    case SmpteFrameRate.Smpte24:
                        minutes = framecount / 1400D;
                        break;
                    case SmpteFrameRate.Smpte25:
                        minutes = framecount / 1500D;
                        break;
                    case SmpteFrameRate.Smpte2997Drop:
                    case SmpteFrameRate.Smpte2997NonDrop:
                    case SmpteFrameRate.Smpte30:
                        minutes = framecount / 1800D;
                        break;
                    default:
                        minutes = framecount / 1800D;
                        break;
                }

                return minutes;
            }
        }

        /// <summary>
        /// Gets the value of the current TimeCode structure expressed in whole
        /// and fractional seconds. Not as Precise as the TotalSecondsPrecision.
        /// </summary>
        /// <returns>
        /// The total number of seconds represented by this instance.
        /// </returns>
        /// <value>The number of seconds of the TimeCode.</value>
        public double TotalSeconds
        {
            get
            {
                return this.AbsoluteTime.TimeAsDouble;
            }
        }

        /// <summary>
        /// Gets the value of the current TimeCode structure expressed in whole
        /// and fractional seconds. This is returned as a <see cref="decimal"/> for greater precision.
        /// </summary>
        /// <returns>
        /// The total number of seconds in <see cref="decimal"/> precision represented by this instance.
        /// </returns>
        /// <value>The number of seconds of the TimeCode.</value>
        public double TotalSecondsPrecision
        {
            get
            {
                return this.AbsoluteTime.TimeAsFloat;
            }
        }

        /// <summary>
        /// Gets the value of the current TimeCode structure expressed in frames.
        /// </summary>
        /// <returns>
        ///  The total number of frames represented by this instance.
        /// </returns>
        /// <value>The total frames of the current TimeCode.</value>
        public ulong TotalFrames
        {
            get
            {
                return AbsoluteTimeToFrames(this.AbsoluteTime, this.frameRate);
            }
        }

        /// <summary>
        /// The private Timespan used to track absolute time for this instance.
        /// </summary>
        private AbsoluteTimeHelper AbsoluteTime
        {
            get
            {
                if (this.absoluteTime == null)
                {
                    this.absoluteTime = new AbsoluteTimeHelper(0);
                }

                return this.absoluteTime;
            }

            set
            {
                this.absoluteTime = value;
            }
        }

        /// <summary>
        /// Subtracts a specified TimeCode from another specified TimeCode.
        /// </summary>
        /// <param name="t1">The first TimeCode.</param>
        /// <param name="t2">The second TimeCode.</param>
        /// <returns>A TimeCode whose value is the result of the value of t1 minus the value of t2.
        /// </returns>
        public static TimeCode operator -(TimeCode t1, TimeCode t2)
        {
            var t3 = new TimeCode(t1.AbsoluteTime.TimeAsDouble - t2.AbsoluteTime.TimeAsDouble, t1.FrameRate);

            if (t3.TotalSeconds < MinValue)
            {
                throw new OverflowException(Resources.MinValueSmpte12MOverflowException);
            }

            return t3;
        }

        /// <summary>
        /// Indicates whether two TimeCode instances are not equal.
        /// </summary>
        /// <param name="t1">The first TimeCode.</param>
        /// <param name="t2">The second TimeCode.</param>
        /// <returns>true if the values of t1 and t2 are not equal; otherwise, false.</returns>
        public static bool operator !=(TimeCode t1, TimeCode t2)
        {
            var timeCode1 = new TimeCode(t1.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);
            var timeCode2 = new TimeCode(t2.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);

            if ((int)timeCode1.TotalSeconds != (int)timeCode2.TotalSeconds)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds two specified TimeCode instances.
        /// </summary>
        /// <param name="t1">The first TimeCode.</param>
        /// <param name="t2">The second TimeCode.</param>
        /// <returns>A TimeCode whose value is the sum of the values of t1 and t2.</returns>
        public static TimeCode operator +(TimeCode t1, TimeCode t2)
        {
            // FIX: TimeCode loses precision if we use frames instead of seconds
            var t3 = new TimeCode(t1.AbsoluteTime.TimeAsDouble + t2.AbsoluteTime.TimeAsDouble, t1.FrameRate);

            //ulong frames = t1.TotalFrames + t2.TotalFrames;
            //var t3 = TimeCode.FromFrames(frames, t1.FrameRate);

            return t3;
        }

        /// <summary>
        ///  Indicates whether a specified TimeCode is less than another
        ///  specified TimeCode.
        /// </summary>
        /// <param name="t1">The first TimeCode.</param>
        /// <param name="t2">The second TimeCode.</param>
        /// <returns> 
        /// A true if the value of t1 is less than the value of t2; otherwise, false.
        /// </returns>
        public static bool operator <(TimeCode t1, TimeCode t2)
        {
            var timeCode1 = new TimeCode(t1.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);
            var timeCode2 = new TimeCode(t2.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);


            if ((int)timeCode1.TotalFrames < (int)timeCode2.TotalFrames)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///  Indicates whether a specified TimeCode is less than or equal to another
        ///  specified TimeCode.
        /// </summary>
        /// <param name="t1">The first TimeCode.</param>
        /// <param name="t2">The second TimeCode.</param>
        /// <returns>true if the value of t1 is less than or equal to the value of t2; otherwise, false.</returns>
        public static bool operator <=(TimeCode t1, TimeCode t2)
        {
            var timeCode1 = new TimeCode(t1.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);
            var timeCode2 = new TimeCode(t2.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);

            if ((int)timeCode1.TotalFrames < (int)timeCode2.TotalFrames || ((int)timeCode1.TotalFrames == (int)timeCode2.TotalFrames))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///  Indicates whether two TimeCode instances are equal.
        /// </summary>
        /// <param name="t1">The first TimeCode.</param>
        /// <param name="t2">The second TimeCode.</param>
        /// <returns>true if the values of t1 and t2 are equal; otherwise, false.</returns>
        public static bool operator ==(TimeCode t1, TimeCode t2)
        {
            var timeCode1 = new TimeCode(t1.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);
            var timeCode2 = new TimeCode(t2.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);

            if ((int)timeCode1.TotalFrames == (int)timeCode2.TotalFrames)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Indicates whether a specified TimeCode is greater than another specified
        ///     TimeCode.
        /// </summary>
        /// <param name="t1">The first TimeCode.</param>
        /// <param name="t2">The second TimeCode.</param>
        /// <returns>true if the value of t1 is greater than the value of t2; otherwise, false.
        /// </returns>
        public static bool operator >(TimeCode t1, TimeCode t2)
        {
            var timeCode1 = new TimeCode(t1.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);
            var timeCode2 = new TimeCode(t2.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);

            if ((int)timeCode1.TotalFrames > (int)timeCode2.TotalFrames)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Indicates whether a specified TimeCode is greater than or equal to
        ///     another specified TimeCode.
        /// </summary>
        /// <param name="t1">The first TimeCode.</param>
        /// <param name="t2">The second TimeCode.</param>
        /// <returns>
        /// A true if the value of t1 is greater than or equal to the value of t2; otherwise,
        ///    false.
        /// </returns>
        public static bool operator >=(TimeCode t1, TimeCode t2)
        {
            var timeCode1 = new TimeCode(t1.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);
            var timeCode2 = new TimeCode(t2.AbsoluteTime.TimeAsDouble, SmpteFrameRate.Smpte30);

            if ((int)timeCode1.TotalFrames > (int)timeCode2.TotalFrames || ((int)timeCode1.TotalFrames == (int)timeCode2.TotalFrames))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a SMPTE 12M formatted time code string from a 27Mhz ticks value.
        /// </summary>
        /// <param name="ticks27Mhz">27Mhz ticks value.</param>
        /// <param name="rate">The SMPTE time code framerated desired.</param>
        /// <returns>A SMPTE 12M formatted time code string.</returns>
        public static string Ticks27MhzToSmpte12M(ulong ticks27Mhz, SmpteFrameRate rate)
        {
            switch (rate)
            {
                case SmpteFrameRate.Smpte2398:
                    return Ticks27MhzToSmpte12M_23_98fps(ticks27Mhz);
                case SmpteFrameRate.Smpte24:
                    return Ticks27MhzToSmpte12M_24fps(ticks27Mhz);
                case SmpteFrameRate.Smpte25:
                    return Ticks27MhzToSmpte12M_25fps(ticks27Mhz);
                case SmpteFrameRate.Smpte2997Drop:
                    return Ticks27MhzToSmpte12M_29_27_Drop(ticks27Mhz);
                case SmpteFrameRate.Smpte2997NonDrop:
                    return Ticks27MhzToSmpte12M_29_27_NonDrop(ticks27Mhz);
                case SmpteFrameRate.Smpte30:
                    return Ticks27MhzToSmpte12M_30fps(ticks27Mhz);
                default:
                    return Ticks27MhzToSmpte12M_30fps(ticks27Mhz);
            }
        }

        /// <summary>
        /// Compares two TimeCode values and returns an integer that indicates their relationship.
        /// </summary>
        /// <param name="t1">The first TimeCode.</param>
        /// <param name="t2">The second TimeCode.</param>
        /// <returns>
        /// Value Condition -1 t1 is less than t2, 0 t1 is equal to t2, 1 t1 is greater than t2.
        /// </returns>
        public static int Compare(TimeCode t1, TimeCode t2)
        {
            if (t1 < t2)
            {
                return -1;
            }

            if (t1 == t2)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        ///  Returns a value indicating whether two specified instances of TimeCode
        ///  are equal.
        /// </summary>
        /// <param name="t1">The first TimeCode.</param>
        /// <param name="t2">The second TimeCode.</param>
        /// <returns>true if the values of t1 and t2 are equal; otherwise, false.</returns>
        public static bool Equals(TimeCode t1, TimeCode t2)
        {
            if (t1 == t2)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///  Returns a TimeCode that represents a specified number of days, where
        ///  the specification is accurate to the nearest millisecond.
        /// </summary>
        /// <param name="value">A number of days accurate to the nearest millisecond.</param>
        /// <param name="rate">The desired framerate for this instance.</param>
        /// <returns> A TimeCode that represents value.</returns>
        public static TimeCode FromDays(double value, SmpteFrameRate rate)
        {
            double absoluteTime = value * TicksPerDayAbsoluteTime;
            return new TimeCode(absoluteTime, rate);
        }

        /// <summary>
        ///  Returns a TimeCode that represents a specified number of hours, where
        ///  the specification is accurate to the nearest millisecond.
        /// </summary>
        /// <param name="value">A number of hours accurate to the nearest millisecond.</param>
        /// <param name="rate">The desired framerate for this instance.</param>
        /// <returns> A TimeCode that represents value.</returns>
        public static TimeCode FromHours(double value, SmpteFrameRate rate)
        {
            float absoluteTime = (float)(value * TicksPerHourAbsoluteTime);
            return new TimeCode(absoluteTime, rate);
        }

        /// <summary>
        ///   Returns a TimeCode that represents a specified number of minutes,
        ///   where the specification is accurate to the nearest millisecond.
        /// </summary>
        /// <param name="value">A number of minutes, accurate to the nearest millisecond.</param>
        /// <param name="rate">The <see cref="SmpteFrameRate"/> to use for the calculation.</param>
        /// <returns>A TimeCode that represents value.</returns>
        public static TimeCode FromMinutes(double value, SmpteFrameRate rate)
        {
            float absoluteTime = (float)(value * TicksPerMinuteAbsoluteTime);
            return new TimeCode(absoluteTime, rate);
        }

        /// <summary>
        /// Returns a TimeCode that represents a specified number of seconds,
        /// where the specification is accurate to the nearest millisecond.
        /// </summary>
        /// <param name="value">A number of seconds, accurate to the nearest millisecond.</param>
        /// /// <param name="rate">The framerate of the Timecode.</param>
        /// <returns>A TimeCode that represents value.</returns>
        public static TimeCode FromSeconds(double value, SmpteFrameRate rate)
        {
            return new TimeCode(value, rate);
        }

        /// <summary>
        /// Returns a TimeCode that represents a specified number of frames.
        /// </summary>
        /// <param name="value">A number of frames.</param>
        /// <param name="rate">The framerate of the Timecode.</param>
        /// <returns>A TimeCode that represents value.</returns>
        public static TimeCode FromFrames(ulong value, SmpteFrameRate rate)
        {
            //ulong ticks27Mhz = FramesToTicks27Mhz(value, rate);
            //return new TimeCode(ticks27Mhz, rate);

            double absoluteTime = FramesToAbsoluteTime(value, rate);
            return new TimeCode(absoluteTime, rate);
        }

        /// <summary>
        /// Returns a TimeCode that represents a specified time, where the specification
        ///  is in units of ticks.
        /// </summary>
        /// <param name="ticks"> A number of ticks that represent a time.</param>
        /// <param name="rate">The Smpte framerate.</param>
        /// <returns>A TimeCode with a value of value.</returns>
        public static TimeCode FromTicks(ulong ticks, SmpteFrameRate rate)
        {
            double absoluteTime = Math.Pow(10, -7) * ticks;
            return new TimeCode(absoluteTime, rate);
        }

        /// <summary>
        /// Returns a TimeCode that represents a specified time, where the specification is 
        /// in units of 27 Mhz clock ticks.
        /// </summary>
        /// <param name="value">A number of ticks in 27 Mhz clock format.</param>
        /// <param name="rate">A Smpte framerate.</param>
        /// <returns>A TimeCode.</returns>
        public static TimeCode FromTicks27Mhz(ulong value, SmpteFrameRate rate)
        {
            return new TimeCode(value, rate);
        }

        /// <summary>
        /// Returns a TimeCode that represents a specified time, where the specification is 
        /// in units of absolute time.
        /// </summary>
        /// <param name="value">The absolute time in 100 nanosecond units.</param>
        /// <param name="rate">The SMPTE framerate.</param>
        /// <returns>A TimeCode.</returns>
        public static TimeCode FromAbsoluteTime(double value, SmpteFrameRate rate)
        {
            return new TimeCode(value, rate);
        }

        /// <summary>
        /// Returns a TimeCode that represents a specified time, where the specification is 
        /// in units of absolute time.
        /// </summary>
        /// <param name="value">The <see cref="TimeSpan"/> object.</param>
        /// <param name="rate">The SMPTE framerate.</param>
        /// <returns>A TimeCode.</returns>
        public static TimeCode FromTimeSpan(TimeSpan value, SmpteFrameRate rate)
        {
            return new TimeCode(value.TotalSeconds, rate);
        }

        /// <summary>
        /// Validates that the string provided is in the correct format for SMPTE 12M time code.
        /// </summary>
        /// <param name="timeCode">String that is the time code.</param>
        /// <returns>True if this is a valid SMPTE 12M time code string.</returns>
        public static bool ValidateSmpte12MTimecode(string timeCode)
        {
            string[] times = timeCode.Split(':', ';');

            int index = -1;
            int days = 0;

            if (times.Length > 4)
            {
                days = Int32.Parse(times[++index], CultureInfo.InvariantCulture);
            }

            int hours = Int32.Parse(times[++index], CultureInfo.InvariantCulture);
            int minutes = Int32.Parse(times[++index], CultureInfo.InvariantCulture);
            int seconds = Int32.Parse(times[++index], CultureInfo.InvariantCulture);
            int frames = Int32.Parse(times[++index], CultureInfo.InvariantCulture);

            if ((days < 0) || (hours >= 24) || (minutes >= 60) || (seconds >= 60) || (frames >= 30))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates that the hexadecimal formatted integer provided is in the correct format for SMPTE 12M time code
        /// Time code is stored so that the hexadecimal value is read as if it were an integer value. 
        /// That is, the time code value 0x01133512 does not represent integer 18035986, rather it specifies 1 hour, 13 minutes, 35 seconds, and 12 frames.      
        /// </summary>
        /// <param name="windowsMediaTimeCode">Integer that is the time code stored in hexadecimal format.</param>
        /// <returns>True if this is a valid SMPTE 12M time code string.</returns>
        public static bool ValidateSmpte12MTimecode(int windowsMediaTimeCode)
        {
            byte[] timeCodeBytes = BitConverter.GetBytes(windowsMediaTimeCode);
            string timeCode = string.Format("{3:x2}:{2:x2}:{1:x2}:{0:x2}", timeCodeBytes[0], timeCodeBytes[1], timeCodeBytes[2], timeCodeBytes[3]);
            string[] times = timeCode.Split(':', ';');

            int hours = Int32.Parse(times[0], CultureInfo.InvariantCulture);
            int minutes = Int32.Parse(times[1], CultureInfo.InvariantCulture);
            int seconds = Int32.Parse(times[2], CultureInfo.InvariantCulture);
            int frames = Int32.Parse(times[3], CultureInfo.InvariantCulture);

            if ((hours >= 24) || (minutes >= 60) || (seconds >= 60) || (frames >= 30))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns the value of the provided time code string and framerate in 27Mhz ticks.
        /// </summary>
        /// <param name="timeCode">The SMPTE 12M formatted time code string.</param>
        /// <param name="rate">The SMPTE framerate.</param>
        /// <returns>A ulong that represents the value of the time code in 27Mhz ticks.</returns>
        public static ulong Smpte12MToTicks27Mhz(string timeCode, SmpteFrameRate rate)
        {
            switch (rate)
            {
                case SmpteFrameRate.Smpte2398:
                    return Smpte12M_23_98fpsToTicks27Mhz(timeCode);
                case SmpteFrameRate.Smpte24:
                    return Smpte12M_24fpsToTicks27Mhz(timeCode);
                case SmpteFrameRate.Smpte25:
                    return Smpte12M_25fpsToTicks27Mhz(timeCode);
                case SmpteFrameRate.Smpte2997Drop:
                    return Smpte12M_29_27_DropToTicks27Mhz(timeCode);
                case SmpteFrameRate.Smpte2997NonDrop:
                    return Smpte12M_29_27_NonDropToTicks27Mhz(timeCode);
                case SmpteFrameRate.Smpte30:
                    return Smpte12M_30fpsToTicks27Mhz(timeCode);
                default:
                    return Smpte12M_30fpsToTicks27Mhz(timeCode);
            }
        }

        /// <summary>
        /// Parses a framerate value as double and converts it to a member of the SmpteFrameRate enumeration.
        /// </summary>
        /// <param name="rate">Double value of the framerate.</param>
        /// <returns>A SmpteFrameRate enumeration value that matches the incoming rates.</returns>
        public static SmpteFrameRate ParseFramerate(double rate)
        {
            int rateRounded = (int)Math.Floor(rate);

            switch (rateRounded)
            {
                case 23: return SmpteFrameRate.Smpte2398;
                case 24: return SmpteFrameRate.Smpte24;
                case 25: return SmpteFrameRate.Smpte25;
                case 29: return SmpteFrameRate.Smpte2997NonDrop;
                case 30: return SmpteFrameRate.Smpte30;
                case 50: return SmpteFrameRate.Smpte25;
                case 60: return SmpteFrameRate.Smpte30;
                case 59: return SmpteFrameRate.Smpte2997NonDrop;
            }

            return SmpteFrameRate.Unknown;
        }

        /// <summary>
        /// Adds the specified TimeCode to this instance.
        /// </summary>
        /// <param name="ts">A TimeCode.</param>
        /// <returns>A TimeCode that represents the value of this instance plus the value of ts.
        /// </returns>
        public TimeCode Add(TimeCode ts)
        {
            return this + ts;
        }

        /// <summary>
        ///  Compares this instance to a specified object and returns an indication of
        ///   their relative values.
        /// </summary>
        /// <param name="value">An object to compare, or null.</param>
        /// <returns>
        ///  Value Condition -1 The value of this instance is less than the value of value.
        ///    0 The value of this instance is equal to the value of value. 1 The value
        ///    of this instance is greater than the value of value.-or- value is null.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        ///  value is not a TimeCode.
        /// </exception>
        public int CompareTo(object value)
        {
            if (!(value is TimeCode))
            {
                throw new ArgumentException(Resources.Smpte12MOutOfRange);
            }

            TimeCode t1 = (TimeCode)value;

            if (this < t1)
            {
                return -1;
            }

            if (this == t1)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// Compares this instance to a specified TimeCode object and returns
        /// an indication of their relative values.
        /// </summary>
        /// <param name="value"> A TimeCode object to compare to this instance.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and value.Value
        /// Description A negative integer This instance is less than value. Zero This
        /// instance is equal to value. A positive integer This instance is greater than
        /// value.
        /// </returns>
        public int CompareTo(TimeCode value)
        {
            if (this < value)
            {
                return -1;
            }

            if (this == value)
            {
                return 0;
            }

            return 1;
        }

        /// <summary>
        ///  Returns a value indicating whether this instance is equal to a specified
        ///  object.
        /// </summary>
        /// <param name="value">An object to compare with this instance.</param>
        /// <returns>
        /// A true if value is a TimeCode object that represents the same time interval
        /// as the current TimeCode structure; otherwise, false.
        /// </returns>
        public override bool Equals(object value)
        {
            if (this == (TimeCode)value)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified
        ///     TimeCode object.
        /// </summary>
        /// <param name="obj">An TimeCode object to compare with this instance.</param>
        /// <returns>true if obj represents the same time interval as this instance; otherwise, false.
        /// </returns>
        public bool Equals(TimeCode obj)
        {
            if (this == obj)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns> A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        /// <summary>
        /// Subtracts the specified TimeCode from this instance.
        /// </summary>
        /// <param name="ts">A TimeCode.</param>
        /// <returns>A TimeCode whose value is the result of the value of this instance minus the value of ts.</returns>
        public TimeCode Subtract(TimeCode ts)
        {
            return this - ts;
        }

        /// <summary>
        /// Returns the SMPTE 12M string representation of the value of this instance.
        /// </summary>
        /// <returns>
        /// A string that represents the value of this instance. The return value is
        ///     of the form: hh:mm:ss:ff for non-drop frame and hh:mm:ss;ff for drop frame code
        ///     with "hh" hours, ranging from 0 to 23, "mm" minutes
        ///     ranging from 0 to 59, "ss" seconds ranging from 0 to 59, and  "ff"  based on the 
        ///     chosen framerate to be used by the time code instance.
        /// </returns>
        public override string ToString()
        {
            return AbsoluteTimeToSmpte12M(this.AbsoluteTime, this.frameRate);
        }

        /// <summary>
        /// Outputs a string of the current time code in the requested framerate.
        /// </summary>
        /// <param name="rate">The SmpteFrameRate required for the string output.</param>
        /// <returns>SMPTE 12M formatted time code string converted to the requested framerate.</returns>
        public string ToString(SmpteFrameRate rate)
        {
            return AbsoluteTimeToSmpte12M(this.AbsoluteTime, rate);
        }

        /// <summary>
        /// Returns the value of this instance in 27 Mhz ticks.
        /// </summary>
        /// <returns>A ulong value that is in 27 Mhz ticks.</returns>
        public ulong ToTicks27Mhz()
        {
            return AbsoluteTimeToTicks27Mhz(this.AbsoluteTime);
        }

        /// <summary>
        /// Returns the value of this instance in MPEG 2 PCR time base (PcrTb) format.
        /// </summary>
        /// <returns>A ulong value that is in PcrTb.</returns>
        public ulong ToTicksPcrTb()
        {
            return AbsoluteTimeToTicksPcrTb(this.AbsoluteTime);
        }

        /// <summary>
        /// Converts a SMPTE timecode to absolute time.
        /// </summary>
        /// <param name="timeCode">The timecode to convert from.</param>
        /// <param name="rate">The <see cref="SmpteFrameRate"/> of the timecode.</param>
        /// <returns>A <see cref="decimal"/> with the absolute time.</returns>
        private static AbsoluteTimeHelper Smpte12mToAbsoluteTime(string timeCode, SmpteFrameRate rate)
        {
            AbsoluteTimeHelper absoluteTime = new AbsoluteTimeHelper(0);

            switch (rate)
            {
                case SmpteFrameRate.Smpte2398:
                    absoluteTime = Smpte12M_23_98_ToAbsoluteTime(timeCode);
                    break;
                case SmpteFrameRate.Smpte24:
                    absoluteTime = Smpte12M_24_ToAbsoluteTime(timeCode);
                    break;
                case SmpteFrameRate.Smpte25:
                    absoluteTime = Smpte12M_25_ToAbsoluteTime(timeCode);
                    break;
                case SmpteFrameRate.Smpte2997Drop:
                    absoluteTime = Smpte12M_29_97_Drop_ToAbsoluteTime(timeCode);
                    break;
                case SmpteFrameRate.Smpte2997NonDrop:
                    absoluteTime = Smpte12M_29_97_NonDrop_ToAbsoluteTime(timeCode);
                    break;
                case SmpteFrameRate.Smpte30:
                    absoluteTime = Smpte12M_30_ToAbsoluteTime(timeCode);
                    break;
            }

            return absoluteTime;
        }

        /// <summary>
        /// Parses a timecode string for the different parts of the timecode.
        /// </summary>
        /// <param name="timeCode">The source timecode to parse.</param>
        /// <param name="days">The Days section from the timecode.</param>
        /// <param name="hours">The Hours section from the timecode.</param>
        /// <param name="minutes">The Minutes section from the timecode.</param>
        /// <param name="seconds">The Seconds section from the timecode.</param>
        /// <param name="frames">The frames section from the timecode.</param>
        private static void ParseTimecodeString(string timeCode, out int days, out int hours, out int minutes, out int seconds, out int frames)
        {
            if (!_validateTimecode.IsMatch(timeCode))
            {
                throw new FormatException(Resources.Smpte12MBadFormat);
            }

            string[] times = timeCode.Split(':', ';');

            int index = -1;

            days = 0;

            if (times.Length > 4)
            {
                days = Int32.Parse(times[++index], CultureInfo.InvariantCulture);
            }

            hours = Int32.Parse(times[++index], CultureInfo.InvariantCulture);
            minutes = Int32.Parse(times[++index], CultureInfo.InvariantCulture);
            seconds = Int32.Parse(times[++index], CultureInfo.InvariantCulture);
            frames = Int32.Parse(times[++index], CultureInfo.InvariantCulture);

            if ((days < 0) || (hours >= 24) || (minutes >= 60) || (seconds >= 60) || (frames >= 30))
            {
                throw new FormatException(Resources.Smpte12MOutOfRange);
            }
        }

        /// <summary>
        /// Parses a timecode string for the different parts of the timecode.
        /// </summary>
        /// <param name="timeCode">The source timecode to parse.</param>
        /// <param name="hours">The Hours section from the timecode.</param>
        /// <param name="minutes">The Minutes section from the timecode.</param>
        /// <param name="seconds">The Seconds section from the timecode.</param>
        /// <param name="frames">The frames section from the timecode.</param>
        private static void ParseTimecodeString(string timeCode, out int hours, out int minutes, out int seconds, out int frames)
        {
            if (!_validateTimecode.IsMatch(timeCode))
            {
                throw new FormatException(Resources.Smpte12MBadFormat);
            }

            string[] times = timeCode.Split(':', ';');

            hours = Int32.Parse(times[0], CultureInfo.InvariantCulture);
            minutes = Int32.Parse(times[1], CultureInfo.InvariantCulture);
            seconds = Int32.Parse(times[2], CultureInfo.InvariantCulture);
            frames = Int32.Parse(times[3], CultureInfo.InvariantCulture);

            if ((hours >= 24) || (minutes >= 60) || (seconds >= 60) || (frames >= 30))
            {
                throw new FormatException(Resources.Smpte12MOutOfRange);
            }
        }

        /// <summary>
        /// Generates a string representation of the timecode.
        /// </summary>
        /// <param name="days">The Days section from the timecode.</param>
        /// <param name="hours">The Hours section from the timecode.</param>
        /// <param name="minutes">The Minutes section from the timecode.</param>
        /// <param name="seconds">The Seconds section from the timecode.</param>
        /// <param name="frames">The frames section from the timecode.</param>
        /// <param name="dropFrame">Indicates whether the timecode is drop frame or not.</param>
        /// <returns>The timecode in string format.</returns>
        private static string FormatTimeCodeString(int days, int hours, int minutes, int seconds, int frames, bool dropFrame)
        {
            string framesSeparator = ":";
            if (dropFrame)
            {
                framesSeparator = ";";
            }

            if (days > 0)
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}:{1:D2}:{2:D2}:{3:D2}{5}{4:D2}", days, hours, minutes, seconds, frames, framesSeparator);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, "{0:D2}:{1:D2}:{2:D2}{4}{3:D2}", hours, minutes, seconds, frames, framesSeparator);
            }
        }

        /// <summary>
        /// Generates a string representation of the timecode.
        /// </summary>
        /// <param name="hours">The Hours section from the timecode.</param>
        /// <param name="minutes">The Minutes section from the timecode.</param>
        /// <param name="seconds">The Seconds section from the timecode.</param>
        /// <param name="frames">The frames section from the timecode.</param>
        /// <param name="dropFrame">Indicates whether the timecode is drop frame or not.</param>
        /// <returns>The timecode in string format.</returns>
        private static string FormatTimeCodeString(int hours, int minutes, int seconds, int frames, bool dropFrame)
        {
            string framesSeparator = ":";
            if (dropFrame)
            {
                framesSeparator = ";";
            }

            return string.Format(CultureInfo.InvariantCulture, "{0:D2}:{1:D2}:{2:D2}{4}{3:D2}", hours, minutes, seconds, frames, framesSeparator);
        }

        /// <summary>
        /// Converts to Absolute time from SMPTE 12M 23.98.
        /// </summary>
        /// <param name="timeCode">The timecode to parse.</param>
        /// <returns>A <see cref="decimal"/> that contains the absolute duration.</returns>
        private static AbsoluteTimeHelper Smpte12M_23_98_ToAbsoluteTime(string timeCode)
        {
            int days, hours, minutes, seconds, frames;

            ParseTimecodeString(timeCode, out days, out hours, out minutes, out seconds, out frames);

            if (frames >= 24)
            {
                throw new FormatException(Resources.Smpte12M_2398_BadFormat);
            }

            ulong framesHertz = (ulong)Math.Ceiling(1001D * (15 / 4D) * frames);
            int secondsHertz = 90090 * seconds;
            int minutesHertz = 5405400 * minutes;
            ulong hoursHertz = (ulong)324324000 * (ulong)hours;
            ulong daysHertz = (ulong)7783776000 * (ulong)days;

            ulong pcrTb = (ulong)framesHertz + (ulong)secondsHertz + (ulong)minutesHertz + (ulong)hoursHertz + (ulong)daysHertz;

            ulong ticks27Mhz = pcrTb * 300L;

            return new AbsoluteTimeHelper(ticks27Mhz);
        }

        /// <summary>
        /// Converts to Absolute time from SMPTE 12M 24.
        /// </summary>
        /// <param name="timeCode">The timecode to parse.</param>
        /// <returns>A <see cref="decimal"/> that contains the absolute duration.</returns>
        private static AbsoluteTimeHelper Smpte12M_24_ToAbsoluteTime(string timeCode)
        {
            int days, hours, minutes, seconds, frames;

            ParseTimecodeString(timeCode, out days, out hours, out minutes, out seconds, out frames);

            if (frames >= 24)
            {
                throw new FormatException(Resources.Smpte12M_24_BadFormat);
            }

            ulong ticks27Mhz = (ulong)((3750L * frames) + (90000L * seconds) + (5400000L * minutes) + (324000000L * hours) + (7776000000L * days)) * 300L;
            return new AbsoluteTimeHelper(ticks27Mhz);
        }

        /// <summary>
        /// Converts to Absolute time from SMPTE 12M 25.
        /// </summary>
        /// <param name="timeCode">The timecode to parse.</param>
        /// <returns>A <see cref="decimal"/> that contains the absolute duration.</returns>
        private static AbsoluteTimeHelper Smpte12M_25_ToAbsoluteTime(string timeCode)
        {
            int days, hours, minutes, seconds, frames;

            ParseTimecodeString(timeCode, out days, out hours, out minutes, out seconds, out frames);

            if (frames >= 25)
            {
                throw new FormatException(Resources.Smpte12M_25_BadFormat);
            }

            ulong ticks27Mhz = (ulong)((3600L * frames) + (90000L * seconds) + (5400000L * minutes) + (324000000L * hours) + (7776000000L * days)) * 300L;
            return new AbsoluteTimeHelper(ticks27Mhz);
        }

        /// <summary>
        /// Converts to Absolute time from SMPTE 12M 29.97 Drop frame.
        /// </summary>
        /// <param name="timeCode">The timecode to parse.</param>
        /// <returns>A <see cref="decimal"/> that contains the absolute duration.</returns>
        private static AbsoluteTimeHelper Smpte12M_29_97_Drop_ToAbsoluteTime(string timeCode)
        {
            int days, hours, minutes, seconds, frames;

            ParseTimecodeString(timeCode, out days, out hours, out minutes, out seconds, out frames);

            if (frames >= 30)
            {
                throw new FormatException(Resources.Smpte12M_2997_Drop_BadFormat);
            }

            double time = (1001 / 30000D) * (frames + (30 * seconds) + (1798 * minutes) + ((2 * (minutes / 10)) + (107892 * hours) + (2589408 * days)));
            return new AbsoluteTimeHelper(time);
        }

        /// <summary>
        /// Converts to Absolute time from SMPTE 12M 29.97 Non Drop.
        /// </summary>
        /// <param name="timeCode">The timecode to parse.</param>
        /// <returns>A <see cref="decimal"/> that contains the absolute duration.</returns>
        private static AbsoluteTimeHelper Smpte12M_29_97_NonDrop_ToAbsoluteTime(string timeCode)
        {
            int days, hours, minutes, seconds, frames;

            ParseTimecodeString(timeCode, out days, out hours, out minutes, out seconds, out frames);

            if (frames >= 30)
            {
                throw new FormatException(Resources.Smpte12M_2997_NonDrop_BadFormat);
            }

            ulong ticks27Mhz = (ulong)((3003L * frames) + (90090L * seconds) + (5405400L * minutes) + (324324000L * hours) + (7783776000L * days)) * 300L;
            return new AbsoluteTimeHelper(ticks27Mhz);
        }

        /// <summary>
        /// Converts to Absolute time from SMPTE 12M 30.
        /// </summary>
        /// <param name="timeCode">The timecode to parse.</param>
        /// <returns>A <see cref="double"/> that contains the absolute duration.</returns>
        private static AbsoluteTimeHelper Smpte12M_30_ToAbsoluteTime(string timeCode)
        {
            int days, hours, minutes, seconds, frames;

            ParseTimecodeString(timeCode, out days, out hours, out minutes, out seconds, out frames);

            if (frames >= 30)
            {
                throw new FormatException(Resources.Smpte12M_30_BadFormat);
            }

            ulong ticks27Mhz = (ulong)((3000L * frames) + (90000L * seconds) + (5400000L * minutes) + (324000000L * hours) + (7776000000L * days)) * 300L;
            return new AbsoluteTimeHelper(ticks27Mhz);
        }

        /// <summary>
        /// Converts from 27Mhz ticks to PCRTb.
        /// </summary>
        /// <param name="ticks27Mhz">The number of 27Mhz ticks to convert from.</param>
        /// <returns>A <see cref="ulong"/> with the PCRTb.</returns>
        private static ulong Ticks27MhzToPcrTb(ulong ticks27Mhz)
        {
            return ticks27Mhz / 300;
        }

        /// <summary>
        ///     Converts the provided absolute time to PCRTb.
        /// </summary>
        /// <param name="time">Absolute time to be converted.</param>
        /// <returns>The number of PCRTb ticks.</returns>
        private static ulong AbsoluteTimeToTicksPcrTb(AbsoluteTimeHelper time)
        {
            return Convert.ToUInt64(time.TimeAsDouble * 90000);
        }

        /// <summary>
        ///     Converts the specified absolute time to 27 mhz ticks.
        /// </summary>
        /// <param name="time">Absolute time to be converted.</param>
        /// <returns>THe number of 27Mhz ticks.</returns>
        private static ulong AbsoluteTimeToTicks27Mhz(AbsoluteTimeHelper time)
        {
            return AbsoluteTimeToTicksPcrTb(time) * 300;
        }

        /// <summary>
        ///     Converts the specified absolute time to absolute time.
        /// </summary>
        /// <param name="ticksPcrTb">Ticks PCRTb to be converted.</param>
        /// <returns>The absolute time.</returns>
        private static double TicksPcrTbToAbsoluteTime(ulong ticksPcrTb)
        {
            return ticksPcrTb / 90000D;
        }

        /// <summary>
        /// Converts the specified absolute time to absolute time.
        /// </summary>
        /// <param name="ticks27Mhz">Ticks 27Mhz to be converted.</param>
        /// <returns>The absolute time.</returns>
        private static double Ticks27MhzToAbsoluteTime(ulong ticks27Mhz)
        {
            ulong ticksPcrTb = Ticks27MhzToPcrTb(ticks27Mhz);
            return TicksPcrTbToAbsoluteTime(ticksPcrTb);
        }

        /// <summary>
        /// Converts to SMPTE 12M.
        /// </summary>
        /// <param name="time">The absolute time to convert from.</param>
        /// <param name="rate">The SMPTE frame rate.</param>
        /// <returns>A string in SMPTE 12M format.</returns>
        private static string AbsoluteTimeToSmpte12M(AbsoluteTimeHelper time, SmpteFrameRate rate)
        {
            string timeCode = String.Empty;

            if (rate == SmpteFrameRate.Smpte2398)
            {
                timeCode = AbsoluteTimeToSmpte12M_23_98fps(time);
            }
            else if (rate == SmpteFrameRate.Smpte24)
            {
                timeCode = AbsoluteTimeToSmpte12M_24fps(time);
            }
            else if (rate == SmpteFrameRate.Smpte25)
            {
                timeCode = AbsoluteTimeToSmpte12M_25fps(time);
            }
            else if (rate == SmpteFrameRate.Smpte2997Drop)
            {
                timeCode = AbsoluteTimeToSmpte12M_29_97_Drop(time);
            }
            else if (rate == SmpteFrameRate.Smpte2997NonDrop)
            {
                timeCode = AbsoluteTimeToSmpte12M_29_97_NonDrop(time);
            }
            else if (rate == SmpteFrameRate.Smpte30)
            {
                timeCode = AbsoluteTimeToSmpte12M_30fps(time);
            }

            return timeCode;
        }

        /// <summary>
        /// Returns the absolute time.
        /// </summary>
        /// <param name="frames">The number of frames.</param>
        /// <param name="rate">The SMPTE frame rate to use for the conversion.</param>
        /// <returns>The absolute time.</returns>
        private static double FramesToAbsoluteTime(float frames, SmpteFrameRate rate)
        {
            double absoluteTimeInDecimal;

            if (rate == SmpteFrameRate.Smpte2398)
            {
                absoluteTimeInDecimal = frames / 24D / (1000 / 1001D);
            }
            else if (rate == SmpteFrameRate.Smpte24)
            {
                absoluteTimeInDecimal = frames / 24D;
            }
            else if (rate == SmpteFrameRate.Smpte25)
            {
                absoluteTimeInDecimal = frames / 25D;
            }
            else if (rate == SmpteFrameRate.Smpte2997Drop || rate == SmpteFrameRate.Smpte2997NonDrop)
            {
                absoluteTimeInDecimal = frames / 30 / (1000 / 1001D);
            }
            else if (rate == SmpteFrameRate.Smpte30)
            {
                absoluteTimeInDecimal = frames / 30D;
            }
            else
            {
                absoluteTimeInDecimal = frames / 30D;
            }

            return absoluteTimeInDecimal;
        }

        /// <summary>
        /// Returns the absolute time.
        /// </summary>
        /// <param name="frames">The number of frames.</param>
        /// <param name="rate">The SMPTE frame rate to use for the conversion.</param>
        /// <returns>The absolute time.</returns>
        private static ulong FramesToTicks27Mhz(float frames, SmpteFrameRate rate)
        {
            ulong ticks27Mhz;

            if (rate == SmpteFrameRate.Smpte2398)
            {
                ticks27Mhz = (ulong)Math.Ceiling(1001D * (15 / 4D) * frames) * 300;
            }
            else if (rate == SmpteFrameRate.Smpte24)
            {
                ticks27Mhz = (ulong)(3750 * frames) * 300;
            }
            else if (rate == SmpteFrameRate.Smpte25)
            {
                ticks27Mhz = (ulong)(3600 * frames) * 300;
            }
            else if (rate == SmpteFrameRate.Smpte2997Drop || rate == SmpteFrameRate.Smpte2997NonDrop)
            {
                ticks27Mhz = (ulong)(3003 * frames) * 300;
            }
            else if (rate == SmpteFrameRate.Smpte30)
            {
                ticks27Mhz = (ulong)(3000 * frames) * 300;
            }
            else
            {
                ticks27Mhz = (ulong)(3000 * frames) * 300;
            }

            return ticks27Mhz;
        }


        /// <summary>
        /// Returns the absolute time.
        /// </summary>
        /// <param name="absoluteTime">The number of frames.</param>
        /// <param name="rate">The SMPTE frame rate to use for the conversion.</param>
        /// <returns>The absolute time.</returns>
        private static ulong AbsoluteTimeToFrames(AbsoluteTimeHelper absoluteTime, SmpteFrameRate rate)
        {
            ulong frames;

            if (rate == SmpteFrameRate.Smpte2398)
            {
                frames = (ulong)((4 / 15M) * (absoluteTime.PcrTime / 1001M));
                return frames;
            }

            if (rate == SmpteFrameRate.Smpte24)
            {
                frames = (ulong)(absoluteTime.PcrTime / 3750);
                return frames;
            }

            if (rate == SmpteFrameRate.Smpte25)
            {
                frames = (ulong)(absoluteTime.PcrTime / 3600);
                return frames;
            }

            if (rate == SmpteFrameRate.Smpte2997Drop || rate == SmpteFrameRate.Smpte2997NonDrop)
            {
                frames = (ulong)(absoluteTime.PcrTime / 3003);
                return frames;
            }

            if (rate == SmpteFrameRate.Smpte30)
            {
                frames = (ulong)(absoluteTime.PcrTime / 3000);
                return frames;
            }

            return (ulong)(absoluteTime.PcrTime / 3003);
        }

        /// <summary>
        /// Returns the SMPTE 12M 23.98 timecode.
        /// </summary>
        /// <param name="absoluteTime">The absolute time to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string AbsoluteTimeToSmpte12M_23_98fps(AbsoluteTimeHelper absoluteTime)
        {
            return Ticks27MhzToSmpte12M_23_98fps(absoluteTime.Time27Mhz);
        }

        /// <summary>
        /// Converts to SMPTE 12M 24fps.
        /// </summary>
        /// <param name="absoluteTime">The absolute time to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string AbsoluteTimeToSmpte12M_24fps(AbsoluteTimeHelper absoluteTime)
        {
            return Ticks27MhzToSmpte12M_24fps(absoluteTime.Time27Mhz);
        }

        /// <summary>
        /// Converts to SMPTE 12M 25fps.
        /// </summary>
        /// <param name="absoluteTime">The absolute time to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string AbsoluteTimeToSmpte12M_25fps(AbsoluteTimeHelper absoluteTime)
        {
            return Ticks27MhzToSmpte12M_25fps(absoluteTime.Time27Mhz);
        }

        /// <summary>
        /// Converts to SMPTE 12M 29.97fps Drop.
        /// </summary>
        /// <param name="absoluteTime">The absolute time to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string AbsoluteTimeToSmpte12M_29_97_Drop(AbsoluteTimeHelper absoluteTime)
        {
            return Ticks27MhzToSmpte12M_29_27_Drop(absoluteTime.Time27Mhz);
        }

        /// <summary>
        /// Converts to SMPTE 12M 29.97fps Non Drop.
        /// </summary>
        /// <param name="absoluteTime">The absolute time to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string AbsoluteTimeToSmpte12M_29_97_NonDrop(AbsoluteTimeHelper absoluteTime)
        {
            return Ticks27MhzToSmpte12M_29_27_NonDrop(absoluteTime.Time27Mhz);
        }

        /// <summary>
        /// Converts to SMPTE 12M 30fps.
        /// </summary>
        /// <param name="absoluteTime">The absolute time to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string AbsoluteTimeToSmpte12M_30fps(AbsoluteTimeHelper absoluteTime)
        {
            return Ticks27MhzToSmpte12M_30fps(absoluteTime.Time27Mhz);
        }

        /// <summary>
        /// Converts to Ticks 27Mhz.
        /// </summary>
        /// <param name="timeCode">The timecode to convert from.</param>
        /// <returns>The number of 27Mhz ticks.</returns>
        private static ulong Smpte12M_30fpsToTicks27Mhz(string timeCode)
        {
            TimeCode t = new TimeCode(timeCode, SmpteFrameRate.Smpte30);
            ulong ticksPcrTb = Convert.ToUInt64((t.FramesSegment * 3000) + (90000 * t.SecondsSegment) + (5400000 * t.MinutesSegment) + (324000000 * t.HoursSegment));
            return ticksPcrTb * 300;
        }

        /// <summary>
        /// Converts to Ticks 27Mhz.
        /// </summary>
        /// <param name="timeCode">The timecode to convert from.</param>
        /// <returns>The number of 27Mhz ticks.</returns>
        private static ulong Smpte12M_23_98fpsToTicks27Mhz(string timeCode)
        {
            TimeCode t = new TimeCode(timeCode, SmpteFrameRate.Smpte2398);
            ulong ticksPcrTb = Convert.ToUInt64((Math.Ceiling(1001 * (15 / 4D) * t.FramesSegment) + (90090 * t.SecondsSegment) + (5405400 * t.MinutesSegment) + (324324000D * t.HoursSegment)));
            return ticksPcrTb * 300;
        }

        /// <summary>
        /// Converts to Ticks 27Mhz.
        /// </summary>
        /// <param name="timeCode">The timecode to convert from.</param>
        /// <returns>The number of 27Mhz ticks.</returns>
        private static ulong Smpte12M_24fpsToTicks27Mhz(string timeCode)
        {
            TimeCode t = new TimeCode(timeCode, SmpteFrameRate.Smpte24);
            ulong ticksPcrTb = Convert.ToUInt64((t.FramesSegment * 3750) + (90000 * t.SecondsSegment) + (5400000 * t.MinutesSegment) + (324000000 * t.HoursSegment));
            return ticksPcrTb * 300;
        }

        /// <summary>
        /// Converts to Ticks 27Mhz.
        /// </summary>
        /// <param name="timeCode">The timecode to convert from.</param>
        /// <returns>The number of 27Mhz ticks.</returns>
        private static ulong Smpte12M_25fpsToTicks27Mhz(string timeCode)
        {
            TimeCode t = new TimeCode(timeCode, SmpteFrameRate.Smpte25);
            ulong ticksPcrTb = Convert.ToUInt64((t.FramesSegment * 3600) + (90000 * t.SecondsSegment) + (5400000 * t.MinutesSegment) + (324000000 * t.HoursSegment));
            return ticksPcrTb * 300;
        }

        /// <summary>
        /// Converts to Ticks 27Mhz.
        /// </summary>
        /// <param name="timeCode">The timecode to convert from.</param>
        /// <returns>The number of 27Mhz ticks.</returns>
        private static ulong Smpte12M_29_27_NonDropToTicks27Mhz(string timeCode)
        {
            TimeCode t = new TimeCode(timeCode, SmpteFrameRate.Smpte2997Drop);
            ulong ticksPcrTb = Convert.ToUInt64((t.FramesSegment * 3003) + (90090 * t.SecondsSegment) + (5405400 * t.MinutesSegment) + (324324000 * t.HoursSegment));
            return ticksPcrTb * 300;
        }

        /// <summary>
        /// Converts to Ticks 27Mhz.
        /// </summary>
        /// <param name="timeCode">The timecode to convert from.</param>
        /// <returns>The number of 27Mhz ticks.</returns>
        private static ulong Smpte12M_29_27_DropToTicks27Mhz(string timeCode)
        {
            TimeCode t = new TimeCode(timeCode, SmpteFrameRate.Smpte2997NonDrop);
            ulong ticksPcrTb = Convert.ToUInt64((3003 * t.FramesSegment) + (90090 * t.SecondsSegment) + (5399394 * t.MinutesSegment) + (6006 * (int)(t.MinutesSegment / 10D)) + (323999676 * t.HoursSegment));
            return ticksPcrTb * 300;
        }

        /// <summary>
        /// Converts to SMPTE 12M 29.27fps Non Drop.
        /// </summary>
        /// <param name="ticks27Mhz">The number of 27Mhz ticks to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string Ticks27MhzToSmpte12M_29_27_NonDrop(ulong ticks27Mhz)
        {
            ulong pcrTb = Ticks27MhzToPcrTb(ticks27Mhz);
            int framecount = (int)(pcrTb / 3003);
            int hours = (int)(framecount / 108000);
            int minutes = (int)((framecount - (108000 * hours)) / 1800);
            int seconds = (int)((framecount - (1800 * minutes) - (108000 * hours)) / 30);
            int frames = framecount - (30 * seconds) - (1800 * minutes) - (108000 * hours);

            int days = hours / 24;
            hours = hours % 24;

            return FormatTimeCodeString(days, hours, minutes, seconds, frames, false);
        }

        /// <summary>
        /// Converts to SMPTE 12M 29.27fps Non Drop.
        /// </summary>
        /// <param name="ticks27Mhz">The number of 27Mhz ticks to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string Ticks27MhzToSmpte12M_29_27_Drop(ulong ticks27Mhz)
        {
            ulong pcrTb = Ticks27MhzToPcrTb(ticks27Mhz);
            int framecount = (int)(pcrTb / 3003);
            int hours = (int)(framecount / 107892);
            int minutes = (int)((framecount + (2 * Convert.ToInt32((framecount - (107892 * hours)) / 1800)) - (2 * Convert.ToInt32((framecount - (107892 * hours)) / 18000)) - (107892 * hours)) / 1800);
            int seconds = (int)((framecount - (1798 * minutes) - (2 * Convert.ToInt32(minutes / 10)) - (107892 * hours)) / 30);
            int frames = framecount - (30 * seconds) - (1798 * minutes) - (2 * Convert.ToInt32(minutes / 10)) - (107892 * hours);

            int days = hours / 24;
            hours = hours % 24;

            return FormatTimeCodeString(days, hours, minutes, seconds, frames, true);
        }

        /// <summary>
        /// Converts to SMPTE 12M 23.98fps.
        /// </summary>
        /// <param name="ticks27Mhz">The number of 27Mhz ticks to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string Ticks27MhzToSmpte12M_23_98fps(ulong ticks27Mhz)
        {
            ulong pcrTb = Ticks27MhzToPcrTb(ticks27Mhz);
            int framecount = (int)((4 / 15D) * (pcrTb / 1001D));
            int days = (int)(framecount / 86400) / 24;
            int hours = (int)((framecount / 86400) % 24);
            int minutes = (int)((framecount - (86400 * hours)) / 1440) % 60;
            int seconds = (int)((framecount - (1440 * minutes) - (86400 * hours)) / 24) % 60;
            int frames = (framecount - (24 * seconds) - (1440 * minutes) - (86400 * hours)) % 24;

            return FormatTimeCodeString(days, hours, minutes, seconds, frames, false);
        }

        /// <summary>
        /// Converts to SMPTE 12M 24fps.
        /// </summary>
        /// <param name="ticks27Mhz">The number of 27Mhz ticks to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string Ticks27MhzToSmpte12M_24fps(ulong ticks27Mhz)
        {
            ulong pcrTb = Ticks27MhzToPcrTb(ticks27Mhz);
            int framecount = (int)(pcrTb / 3750);
            int days = (int)(framecount / 86400) / 24;
            int hours = (int)((framecount / 86400) % 24);
            int minutes = (int)((framecount - (86400 * hours)) / 1440) % 60;
            int seconds = (int)((framecount - (1440 * minutes) - (86400 * hours)) / 24) % 60;
            int frames = (framecount - (24 * seconds) - (1440 * minutes) - (86400 * hours)) % 24;

            return FormatTimeCodeString(days, hours, minutes, seconds, frames, false);
        }

        /// <summary>
        /// Converts to SMPTE 12M 25fps.
        /// </summary>
        /// <param name="ticks27Mhz">The number of 27Mhz ticks to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string Ticks27MhzToSmpte12M_25fps(ulong ticks27Mhz)
        {
            ulong pcrTb = Ticks27MhzToPcrTb(ticks27Mhz);
            int framecount = (int)(pcrTb / 3600);
            int days = (int)((framecount / 90000) / 24);
            int hours = (int)((framecount / 90000) % 24);
            int minutes = (int)((framecount - (90000 * hours)) / 1500) % 60;
            int seconds = (int)((framecount - (1500 * minutes) - (90000 * hours)) / 25) % 60;
            int frames = (framecount - (25 * seconds) - (1500 * minutes) - (90000 * hours)) % 25;

            return FormatTimeCodeString(days, hours, minutes, seconds, frames, false);
        }

        /// <summary>
        /// Converts to SMPTE 12M 30fps.
        /// </summary>
        /// <param name="ticks27Mhz">The number of 27Mhz ticks to convert from.</param>
        /// <returns>A string that contains the correct format.</returns>
        private static string Ticks27MhzToSmpte12M_30fps(ulong ticks27Mhz)
        {
            ulong pcrTb = Ticks27MhzToPcrTb(ticks27Mhz);
            int framecount = (int)(pcrTb / 3000);
            int days = (int)((framecount / 108000) / 24);
            int hours = (int)((framecount / 108000) % 24);
            int minutes = (int)((framecount - (108000 * hours)) / 1800) % 60;
            int seconds = (int)((framecount - (1800 * minutes) - (108000 * hours)) / 30) % 60;
            int frames = (framecount - (30 * seconds) - (1800 * minutes) - (108000 * hours)) % 30;

            return FormatTimeCodeString(days, hours, minutes, seconds, frames, false);
        }
    }
}