﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicConverterTest
{
    /// <summary>
    /// Good Brother Implementation.
    /// </summary>
    public class GoodBrother1 : IGoodBrother,ICompiler
    {
        private List<Note> notes;
        private BPMChanges bpmChanges;
        private MeasureChanges measureChanges;
        private int tapNumber;
        private int breakNumber;
        private int holdNumber;
        private int slideNumber;
        private int touchNumber;
        private int thoNumber;
        private int[] unitScore = { 500,1000,1500,2500 };
        private int achievement = 0;
        private List<List<Note>> chart;
        private Dictionary<string, string> information;
        private readonly string[] TapTypes = { "TAP","STR","TTP", "XTP","XST" };
        private readonly string[] HoldTypes = { "HLD", "THO", "XHO" };
        private readonly string[] SlideTypes = { "SI_", "SV_", "SF_", "SCL", "SCR", "SUL", "SUR", "SLL", "SLR", "SXL", "SXR", "SSL", "SSR" };

        ///Theoritical Rating = (Difference in 100-down and Max score)/100-down

        /// <summary>
        /// Access to Notes
        /// </summary>
        public List<Note> Notes
        {
            get
            {
                return this.notes;
            }
        }

        public List<List<Note>> Chart
        {
            get { return this.chart; }
        }

        /// <summary>
        /// Access to BPM Changes
        /// </summary>
        public BPMChanges BPMChanges
        {
            get
            {
                return this.bpmChanges;
            }
        }

        /// <summary>
        /// Access to Measure Changes
        /// </summary>
        public MeasureChanges MeasureChanges
        {
            get
            {
                return this.measureChanges;
            }
        }

        /// <summary>
        /// Access to Tap Number
        /// </summary>
        public int TapNumber
        {
            get
            {
                return this.tapNumber;
            }
        }

        /// <summary>
        /// Access to Break Number
        /// </summary>
        public int BreakNumber
        {
            get
            {
                return this.breakNumber;
            }
        }

        /// <summary>
        /// Access to Hold Number
        /// </summary>
        public int HoldNumber
        {
            get
            {
                return this.holdNumber;
            }
        }

        /// <summary>
        /// Access to Slide Number
        /// </summary>
        public int SlideNumber
        {
            get
            {
                return this.slideNumber;
            }
        }

        /// <summary>
        /// Access to Touch Number
        /// </summary>
        public int TouchNumber
        {
            get
            {
                return this.touchNumber;
            }
        }

        /// <summary>
        /// Access to Touch Hold Number
        /// </summary>
        public int ThoNumber
        {
            get
            {
                return this.thoNumber;
            }
        }

        /// <summary>
        /// Access to Unit Score
        /// </summary>
        public int[] UnitScore
        {
            get
            {
                return this.unitScore;
            }
        }

        /// <summary>
        /// Access to theoritical Achievement
        /// </summary>
        public int Achievement
        {
            get
            {
                return this.achievement;
            }
        }

        public GoodBrother1()
        {
            this.notes = new List<Note>();
            this.bpmChanges = new BPMChanges();
            this.measureChanges = new MeasureChanges();
            this.chart = new List<List<Note>>();
            this.information = new Dictionary<string, string>();
        }

        /// <summary>
        /// Construct Good Brother with given notes, bpm change definitions and measure change definitions.
        /// </summary>
        /// <param name="notes">Notes in Good Brother</param>
        /// <param name="bpmChanges">BPM Changes: Initial BPM is NEEDED!</param>
        /// <param name="measureChanges">Measure Changes: Initial Measure is NEEDED!</param>
        public GoodBrother1(List<Note> notes, BPMChanges bpmChanges, MeasureChanges measureChanges)
        {
            this.notes = notes;
            this.bpmChanges = bpmChanges;
            this.measureChanges = measureChanges;
            this.chart = new List<List<Note>>();
            this.information = new Dictionary<string, string>();
            this.Update();
        }

        /// <summary>
        /// Construct GoodBrother from location specified
        /// </summary>
        /// <param name="location">MA2 location</param>
        public GoodBrother1(string location)
        {
            string[] tokens = new Tokenizer().Tokens(location);
            GoodBrother1 takenIn = new Parser().GoodBrotherOfToken(tokens);
            this.notes = takenIn.Notes;
            this.bpmChanges = takenIn.BPMChanges;
            this.measureChanges = takenIn.MeasureChanges;
            this.chart = new List<List<Note>>();
            this.information = new Dictionary<string, string>();
            this.Update();
        }

        /// <summary>
        /// Construct GoodBrother with tokens given
        /// </summary>
        /// <param name="tokens">Tokens given</param>
        public GoodBrother1(string[] tokens)
        {
            GoodBrother1 takenIn = new Parser().GoodBrotherOfToken(tokens);
            this.notes = takenIn.Notes;
            this.bpmChanges = takenIn.BPMChanges;
            this.measureChanges = takenIn.MeasureChanges;
            this.chart = new List<List<Note>>();
            this.information = new Dictionary<string, string>();
            this.Update();
        }

        /// <summary>
        /// Construct GoodBrother with existing values
        /// </summary>
        /// <param name="takenIn">Existing good brother</param>
        public GoodBrother1(GoodBrother1 takenIn)
        {
            this.notes = takenIn.Notes;
            this.bpmChanges = takenIn.BPMChanges;
            this.measureChanges = takenIn.MeasureChanges;
            this.chart = new List<List<Note>>();
            this.information = new Dictionary<string, string>();
            this.Update();
        }

        /// <summary>
        /// Check if every item is valid for exporting
        /// </summary>
        /// <returns>True if every element is valid, false elsewise</returns>
        public bool CheckValidity()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update properties in Good Brother for exporting
        /// </summary>
        public void Update()
        {          
            int maxBar=notes[notes.Count-1].Bar;
            for (int i=0;i<=maxBar;i++)
            {
                List<Note> bar = new List<Note>();
                BPMChange noteChange = new BPMChange();
                double currentBPM=this.BPMChanges.ChangeNotes[0].BPM;
                Note lastNote=new Rest();
                foreach (BPMChange x in this.BPMChanges.ChangeNotes)
                {
                    if (x.Bar == i)
                    {
                        bar.Add(x);
                    }
                }
                foreach (Note x in this.Notes)
                {            
                    if (x.Bar == i)
                    {
                        switch (x.NoteSpecificGenre())
                        {
                            case "BPM":
                                currentBPM = x.BPM;
                                break;
                            case "MEASURE":
                                break;
                            case "REST":
                                break;
                            case "TAP":
                                this.tapNumber++;
                                if (x.NoteType.Equals("TTP"))
                                {
                                    this.touchNumber++;
                                }
                                break;
                            case "HOLD":
                                this.holdNumber++;
                                if (x.NoteType.Equals("THO"))
                                {
                                    this.touchNumber++;
                                }
                                break;
                            case "SLIDE_START":
                                this.touchNumber++;
                                break;
                            case "SLIDE":
                                this.slideNumber++;
                                break;
                            default:
                                break;
                        }
                        x.BPM = currentBPM;
                        x.Prev = lastNote;
                        lastNote.Next = x;
                        bar.Add(x);
                        if (!x.NoteSpecificGenre().Equals("SLIDE"))
                        {
                            lastNote = x;
                        }
                    }
                }
                
                List<Note> afterBar = new List<Note>();
                afterBar.Add(new MeasureChange(i, 0, CalculateQuaver(CalculateLeastMeasure(bar))));
                //Console.WriteLine();
                //Console.WriteLine("In bar "+i+", LeastMeasure is "+ CalculateLeastMeasure(bar)+", so quaver will be "+ CalculateQuaver(CalculateLeastMeasure(bar)));
                afterBar.AddRange(bar);
                this.chart.Add(FinishBar(afterBar, this.BPMChanges.ChangeNotes, i,CalculateQuaver(CalculateLeastMeasure(bar))));
            }
            bool hasBPMChange = false;
            foreach (List<Note> bar in this.chart)
            {
                foreach (Note x in bar)
                {
                    if (x.NoteType.Equals("BPM"))
                    {
                        hasBPMChange = hasBPMChange || x.NoteType.Equals("BPM");
                    }
                }
            }
        }

        public string Compose()
        {
            string result = "";
            const string header1 = "VERSION\t0.00.00\t1.03.00\nFES_MODE\t0\n";
            const string header2 = "RESOLUTION\t384\nCLK_DEF\t384\nCOMPATIBLE_CODE\tMA2\n";
            result += header1;
            result += bpmChanges.InitialChange;
            result += measureChanges.InitialChange;
            result += header2;
            result += "\n";

            result += bpmChanges.Compose();
            result += measureChanges.Compose();
            result += "\n";

            foreach (Note x in notes)
            {
                result+=x.Compose(1)+"\n";
            }
            result += "\n";
            return result;
        }

        /// <summary>
        /// Override and compose with given arrays
        /// </summary>
        /// <param name="bpm">Override BPM array</param>
        /// <param name="measure">Override Measure array</param>
        /// <returns>Good Brother with override array</returns>
        public string Compose(BPMChanges bpm, MeasureChanges measure)
        {
            string result = "";
            const string header1 = "VERSION\t0.00.00\t1.03.00\nFES_MODE\t0\n";
            const string header2 = "RESOLUTION\t384\nCLK_DEF\t384\nCOMPATIBLE_CODE\tMA2\n";
            result += header1;
            result += bpm.InitialChange;
            result += measure.InitialChange;
            result += header2;
            result += "\n";

            result+=bpm.Compose();
            result+=measure.Compose();
            result += "\n";

            foreach (Note y in notes)
            {
                result += y.Compose(1) + "\n";
            }
            result += "\n";
            return result;
        }

        /// <summary>
        /// Return the least none 0 measure of bar.
        /// </summary>
        /// <param name="bar">bar to take in</param>
        /// <returns>List none 0 measure</returns>
        public static int CalculateLeastMeasure(List<Note> bar)
        {
            List<int> startTimeList = new List<int>();
            startTimeList.Add(0);
            foreach (Note x in bar)
            {
                if (!startTimeList.Contains(x.StartTime))
                {
                    startTimeList.Add(x.StartTime);
                }
                if (x.NoteType.Equals("BPM"))
                {
                    Console.WriteLine(x.Compose(0));
                }
            }
            if (startTimeList[startTimeList.Count - 1] != 384)
            {
                startTimeList.Add(384);
            }
            List<int> intervalCandidates = new List<int>();
            for (int i = 1; i < startTimeList.Count; i++)
            {
                if (startTimeList[i] - startTimeList[i - 1] > 0)
                {
                    intervalCandidates.Add(startTimeList[i] - startTimeList[i - 1]);
                }
            }
            if (intervalCandidates.Min() == 0)
            {
                throw new Exception("Error: Least interval was 0");
            }
            int minimalInterval = intervalCandidates.Min();
            if (minimalInterval == 0)
            {
                throw new Exception("Error: Note number does not match in bar "+bar[0].Bar);
            }
            bool primeInterval = false;
            bool notAllDivisible = true;
                foreach (int num in intervalCandidates)
                {
                    notAllDivisible = notAllDivisible || num % minimalInterval != 0;
                    if (IsPrime(num))
                    {
                        minimalInterval = 1;
                        primeInterval = true;
                        notAllDivisible = false;
                    }
                    else if (!primeInterval)
                    {
                        if (minimalInterval != 0 && (num % minimalInterval) != 0)
                        {
                            if (GCD(num, minimalInterval)!=1)
                            {
                                minimalInterval /= GCD(minimalInterval,num);
                            }
                            else
                            {
                                minimalInterval = 1;
                                primeInterval=true;
                                notAllDivisible=false;
                            }
                        }
                    }
                }
            return minimalInterval;
            //return 1;
        }

        /// <summary>
        /// Return note number except Rest, BPM and Measure.
        /// </summary>
        /// <param name="Bar">bar of note to take in</param>
        /// <returns>Number</returns>
        public static int RealNoteNumber(List<Note> Bar)
        {
            int result = 0;
            foreach(Note x in Bar)
            {
                if (x.IsNote())
                {
                    result++;
                }
            }
            return result;
        }


        public static bool ContainNotes(List<Note> Bar)
        {
            bool result =false;
            foreach(Note x in Bar)
            {
                result = result || x.IsNote();
            }
            return result;
        }

        /// <summary>
        /// Generate appropriate length for hold and slide.
        /// </summary>
        /// <param name="length">Last Time</param>
        /// <returns>[Definition:Length]=[Quaver:Beat]</returns>
        public static int CalculateQuaver(int length)
        {
            int result = 0;
            const int definition = 384;
            int divisor = GCD(definition, length);
            int quaver = definition / divisor, beat = length / divisor;
            result = quaver;
            return result;
        }

        /// <summary>
        /// Finish Bar writing byu adding specific rest note in between.
        /// </summary>
        /// <param name="bar">Bar to finish with</param>
        /// <param name="bpmChanges">BPMChange Notes</param>
        /// <param name="barNumber">Bar number of Bar</param>
        /// <param name="minimalQuaver">Minimal interval calculated from bar</param>
        /// <returns>Finished bar</returns>
        public static List<Note> FinishBar(List<Note> bar, List<BPMChange> bpmChanges, int barNumber, int minimalQuaver)
        {
            List<Note> result = new List<Note>();
            //Console.WriteLine("The taken in minimal interval is "+minimalQuaver);
            //foreach (BPMChange x in bpmChanges)
            //{
            //    if (x.Bar == barNumber && x.NoteGenre().Equals("BPM"))
            //    {
            //        result.Add(x);
            //        Console.WriteLine("A BPMChange was found and locate in bar" + x.Bar + " in tick " + x.StartTime);
            //    }
            //} 
            bool writeRest = true;
            result.Add(bar[0]);
            for (int i = 0;i<384;i+=384/minimalQuaver)
            {
                List<Note> eachSet = new List<Note>();
                writeRest = true;
                foreach(Note x in bar)
                {
                    //Console.Write("c1: " + (x.StartTime == i));
                    //Console.Write("; c2: " + (x.IsNote()));
                    //Console.Write("; c3: " + (x.NoteSpecificGenre().Equals("MEASURE")));
                    //Console.Write("; FINAL: " + ((x.StartTime == i && x.IsNote()) || x.NoteSpecificGenre().Equals("MEASURE")));
                    if ((x.StartTime == i)&&x.IsNote())
                    {
                        eachSet.Add(x);
                        //Console.WriteLine("A note was found at tick " + i + " of bar " + barNumber + ", it is "+x.NoteType);
                        writeRest = false ;
                    }
                }
                foreach (BPMChange x in bpmChanges)
                {
                    if (eachSet.Contains(x))
                    {
                        eachSet.Remove(x);
                        List<Note> adjusted = new List<Note>();
                        adjusted.Add(x);
                        adjusted.AddRange(eachSet);
                        eachSet = adjusted;
                    }
                }
                //for (int index = 0;index<eachSet.Count;index++)
                //{
                //    if (eachSet[index].NoteSpecificGenre().Equals("BPM"))
                //    {
                //        List<Note> adjusted = new List<Note>();
                //        adjusted.Add(eachSet[index]);
                //        eachSet.RemoveAt(index);
                //        adjusted.AddRange(eachSet);
                //        eachSet = adjusted;
                //    }
                //}
                if (writeRest)
                {
                    //Console.WriteLine("There is no note at tick " + i + " of bar " + barNumber + ", Adding one");
                    eachSet.Add(new Rest("RST", barNumber, i));
                }
                result.AddRange(eachSet);
            }
            if (RealNoteNumber(result)!=RealNoteNumber(bar))
            {
                string error = "";
                error+=("Bar notes not match in bar: "+barNumber);
                error += ("Expected: "+RealNoteNumber(bar));
                foreach (Note x in bar)
                {
                    error += (x.Compose(1));
                }
                error += ("\nActrual: "+RealNoteNumber(result));
                foreach (Note y in result)
                {
                    error += (y.Compose(1));
                }
                Console.WriteLine(error);
                throw new Exception("NOTE NUMBER IS NOT MATCHING");            
            }
            //result.Sort();
            //if (RealNoteNumber(result)==0)
            //{
            //    Console.WriteLine("There is no note at tick " + 0 + " of bar " + barNumber + ", Adding one");
            //    result.Add(new Rest("RST", barNumber,0));
            //}
            if (result[1].NoteSpecificGenre().Equals("BPM"))
            {
                Note temp = result[0];
                result[0] = result[1];
                result[1] = temp;
            }
            return result;
        }

        /// <summary>
        /// Return GCD of A and B.
        /// </summary>
        /// <param name="a">A</param>
        /// <param name="b">B</param>
        /// <returns>GCD of A and B</returns>
        static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        /// <summary>
        /// Return if this is a prime (1 counts)
        /// </summary>
        /// <param name="number">Number to inspect</param>
        /// <returns>True if is prime, false elsewise</returns>
        public static bool IsPrime(int number)
        {
            if (number < 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        /// <summary>
        /// Take in and replace the current information.
        /// </summary>
        /// <param name="information">Dicitionary containing information needed</param>
        public void TakeInformation(Dictionary<string, string> information)
        {
            foreach(KeyValuePair<string, string> x in information)
            {
                this.information.Add(x.Key,x.Value);
            }
        }
    }
}
