using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

// http://www.codeproject.com/Articles/418082/Parsing-EDI-to-XML-and-vice-verse
// https://x12parser.codeplex.com/
// http://edifabric.com/x12/
// https://github.com/DonZoeggerle/ediFabric/releases



namespace EDISAX
{
    public class EdiSax
    {
        public string firstContent = "ISA*00**00**16*102096559TEST *14*PARTNERTEST*071214*1406*U*00204*810000263*1*T*>~\n" +
            "GS* IN*102096559TEST* PARTNER*20071214*1406*810000263*X*004010~\n" +
            "ST*810*166061414~\n" +
            "BIG**0013833070**V8748745*** DI~\n" +
            "NTE*GEN ~\n" +
            "CTT*1\n" +
            "SE*44*166061414~\n" +
            "GE*1*810000263~\n" +
            "IEA*1*810000263~";

        string DataElementSeparator;                // *
        string ComponentDataElementSeparator;       // >
        string RepetitionSeparator;                 // ^
        string SegmentTerminator;                   // ~
        public void Testing()
        {
            string contents = firstContent;
            DataElementSeparator = contents[3].ToString();
            string isa = string.Concat(contents.Take(106));
            string[] isaElements = isa.Split(DataElementSeparator[0]);
            ComponentDataElementSeparator = string.Concat(isaElements[16].First());
            RepetitionSeparator = isaElements[11] != "U" ? isaElements[11] : "^";
            SegmentTerminator = string.Concat(isaElements[16].Skip(1).First());
            if (SegmentTerminator == " " || string.IsNullOrEmpty(SegmentTerminator) ||
                SegmentTerminator == "G")
            {
                SegmentTerminator = Environment.NewLine;
            }

            Debug.WriteLine("DataElementSeparator = " + DataElementSeparator);
            Debug.WriteLine("ComponentDataElementSeparator = " + ComponentDataElementSeparator);
            Debug.WriteLine("RepetitionSeparator = " + RepetitionSeparator);
            Debug.WriteLine("SegmentTerminator = " + SegmentTerminator);
            Debug.WriteLine("");
            Debug.WriteLine("");

        }
    }
}