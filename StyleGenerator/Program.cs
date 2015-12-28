using System;
using System.Text;
using System.IO;

using BibTeX.Translator;
using BibManFunctionality;
using BibToHtml.Converter.Styles;
using BibTeXtoHTML_Win.Project;
using BibTeX.Translator.AuthorFieldAnalizer;

namespace StyleGenerator
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            Console.WriteLine ("Kreator Stylu");
            Console.WriteLine ("Tworzenie stylu abbrv");
            CreateAbbrvStyle ();
            Console.WriteLine ("Styl abbrv utworzony");

        }

        // tworzy styl 'abbrv'
        static void CreateAbbrvStyle()
        {
            BibTeXtoHTML_Win.StyleFormatter.BibTeXtoHTML_Style style = 
                new BibTeXtoHTML_Win.StyleFormatter.BibTeXtoHTML_Style ();


            BibTeXTranslator tr = new BibTeXTranslator ();

            BibStyle htmlStyle = new BibStyle ();

            //
            // Styl BibTeX
            //
            style.BibTeXTranslator = tr;
            tr.LaTeXFunctions.Clear ();
            CreateAlphabet (tr);
            CreateStandardJustPassFunctions (tr);
            CreateAuthorFivLJ (tr);

            //
            // Styl HTML
            //
            style.HtmlExporterStyle = htmlStyle;
            htmlStyle.StyleName = "abbrv";
            // Sortowanie pozycji
            PositionSortObject pso = new PositionSortObject (PositionSort.BySelectedFieldAsc, "author");
            htmlStyle.SortOrder.Add (pso);

            // Domyślny styl pozycji
            PositionStyle defPos = new PositionStyle (PositionStyleType.global);
            // domyślny styl pola
            FieldStyle defFieldDef = new FieldStyle ();
            defFieldDef.UseIt = false;      // domyślnie nie używaj pola
            defPos.DefaultFieldStyle = defFieldDef;
            // Sortowanie pól
            FieldSortObject defFieldSort = new FieldSortObject (FieldSort.ByPositionedFieldStyle);
            defPos.FieldSortObjects.Add(defFieldSort);
            // pozycjonowany styl pól
            // 1
            PositionedFieldStyle positionedFieldStyle1 = new PositionedFieldStyle ();
            positionedFieldStyle1.FieldsOnPosition.Add ("author");
            positionedFieldStyle1.FieldsOnPosition.Add ("edithor");
            FieldStyle fieldStyle1 = new FieldStyle ();
            fieldStyle1.UseIt = true;
            fieldStyle1.Tags.Add (BibToHtml.SupportedHtmlTags.b);
            positionedFieldStyle1.StyleForThisFieldPosition = fieldStyle1;
            defFieldSort.PositionedFieldStyles.Add (positionedFieldStyle1);
            // 2
            PositionedFieldStyle positionedFieldStyle2 = new PositionedFieldStyle ();
            positionedFieldStyle2.FieldsOnPosition.Add ("title");
            FieldStyle fieldStyle2 = new FieldStyle ();
            fieldStyle2.UseIt = true;
            fieldStyle2.Tags.Add (BibToHtml.SupportedHtmlTags.i);
            positionedFieldStyle2.StyleForThisFieldPosition = fieldStyle2;
            defFieldSort.PositionedFieldStyles.Add (positionedFieldStyle2);
            // 3
            PositionedFieldStyle positionedFieldStyle3 = new PositionedFieldStyle ();
            positionedFieldStyle3.FieldsOnPosition.Add ("journal");
            positionedFieldStyle3.FieldsOnPosition.Add ("booktitle");
            FieldStyle fieldStyle3 = new FieldStyle ();
            fieldStyle3.UseIt = true;
            positionedFieldStyle3.StyleForThisFieldPosition = fieldStyle3;
            defFieldSort.PositionedFieldStyles.Add (positionedFieldStyle3);
            // 4
            PositionedFieldStyle positionedFieldStyle4 = new PositionedFieldStyle ();
            positionedFieldStyle4.FieldsOnPosition.Add ("publisher");
            FieldStyle fieldStyle4 = new FieldStyle ();
            fieldStyle4.UseIt = true;
            positionedFieldStyle4.StyleForThisFieldPosition = fieldStyle4;
            defFieldSort.PositionedFieldStyles.Add (positionedFieldStyle4);
            // 5
            PositionedFieldStyle positionedFieldStyle5 = new PositionedFieldStyle ();
            positionedFieldStyle5.FieldsOnPosition.Add ("month");
            FieldStyle fieldStyle5 = new FieldStyle ();
            fieldStyle5.UseIt = true;
            positionedFieldStyle5.StyleForThisFieldPosition = fieldStyle5;
            defFieldSort.PositionedFieldStyles.Add (positionedFieldStyle5);
            // 6
            PositionedFieldStyle positionedFieldStyle6 = new PositionedFieldStyle ();
            positionedFieldStyle6.FieldsOnPosition.Add ("year");
            positionedFieldStyle6.FieldsOnPosition.Add ("date");
            FieldStyle fieldStyle6 = new FieldStyle ();
            fieldStyle6.UseIt = true;
            positionedFieldStyle6.StyleForThisFieldPosition = fieldStyle6;
            defFieldSort.PositionedFieldStyles.Add (positionedFieldStyle6);

            // dodanie ustawień do stylu
            htmlStyle.DefaultPositionStyle = defPos;

            style.WriteThisToXmlFile ("abbrv.xst");
        }

        // tworzy styl 'alpha'
        static void CreateAlphaStyle()
        {

        }

        // tworzy styl 'plain'
        static void CreatePlainStyle()
        {

        }

        // tworzy styl 'unsrt'
        static void CreateUnsrtStyle()
        {

        }


        static void CreateAuthorFivLJ(BibTeXTranslator translator)
        {
            AuthorFieldAnalizer author = new AuthorFieldAnalizer ();
            author.FieldName = "author";
            author.Conjunction = "oraz";
            author.Separator = ",";
            // 1 Imiona
            AuthorFieldFormatObject affo1 = new AuthorFieldFormatObject (AuthorNamePart.FirstName);
            affo1.Initials = true;
            affo1.NumerOfPositions = -1;
            affo1.SpaceBetweenInitials = true;
            author.FormatObjects.Add (affo1);
            // 2 von
            AuthorFieldFormatObject affo2 = new AuthorFieldFormatObject (AuthorNamePart.von);
            affo2.Initials = false;
            affo2.NumerOfPositions = -1;
            affo2.SpaceBetweenInitials = true;
            author.FormatObjects.Add (affo2);
            // 3 Nazwiska
            AuthorFieldFormatObject affo3 = new AuthorFieldFormatObject (AuthorNamePart.LastName);
            affo3.Initials = false;
            affo3.NumerOfPositions = -1;
            affo3.SpaceBetweenInitials = true;
            author.FormatObjects.Add (affo3);
            //  Jr
            AuthorFieldFormatObject affo4 = new AuthorFieldFormatObject (AuthorNamePart.Jr);
            affo4.Initials = false;
            affo4.NumerOfPositions = -1;
            affo4.SpaceBetweenInitials = true;
            author.FormatObjects.Add (affo4);

            AuthorFieldAnalizer editor = author.CloneAsType ();
            editor.FieldName = "editor";

            translator.AuthorFieldAnalizers.Add (author);
            translator.AuthorFieldAnalizers.Add (editor);
        }

        static void CreateAlphabet(BibTeXTranslator translator)
        {
            LaTeXFunction ogonek = new LaTeXFunction ();
            ogonek.Name = "k";
            ogonek.ContentFormat = true;
            ogonek.Contents.Add ("a", "ą");
            ogonek.Contents.Add ("A", "Ą");
            ogonek.Contents.Add ("e", "ę");
            ogonek.Contents.Add ("E", "Ę");
            translator.LaTeXFunctions.Add (ogonek);

            LaTeXFunction dot = new LaTeXFunction ();
            dot.Name = ".";
            dot.ContentFormat = true;
            dot.Contents.Add ("z", "ż");
            dot.Contents.Add ("Z", "Ż");
            translator.LaTeXFunctions.Add (dot);

            LaTeXFunction acute = new LaTeXFunction ();
            acute.Name = "'";
            acute.ContentFormat = true;
            acute.Contents.Add ("c", "ć");
            acute.Contents.Add ("C", "Ć");
            acute.Contents.Add ("e", "é");
            acute.Contents.Add ("E", "É");
            acute.Contents.Add ("n", "ń");
            acute.Contents.Add ("N", "Ń");
            acute.Contents.Add ("o", "ó");
            acute.Contents.Add ("O", "Ó");
            acute.Contents.Add ("s", "ś");
            acute.Contents.Add ("S", "Ś");
            acute.Contents.Add ("z", "ź");
            acute.Contents.Add ("Z", "Ź");
            translator.LaTeXFunctions.Add (acute);

            LaTeXFunction l = new LaTeXFunction ();
            l.Name = "l";
            l.ContentFormat = true;
            l.DefaultContentFormatString = "ł";
            translator.LaTeXFunctions.Add (l);

            LaTeXFunction L = new LaTeXFunction ();
            L.Name = "L";
            L.ContentFormat = true;
            L.DefaultContentFormatString = "Ł";
            translator.LaTeXFunctions.Add (L);
        }

        static void CreateStandardJustPassFunctions(BibTeXTranslator translator)
        {
            LaTeXFunction mbox = new LaTeXFunction();
            mbox.ContentFormat = true;
            mbox.DefaultContentFormatString = "{1}";
            translator.LaTeXFunctions.Add(mbox);
        }

    }
}
