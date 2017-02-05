using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*
function WordCount(text) { 
	var wc = text.split(" ").length;
	return parseFloat(wc);
}

function SentenceCount(text)
{
	var sc = text.split(".").length;
	return parseFloat(sc);
}

function ComplexWordCount(text)
{
	var words = text.split(" ");
	var complex = []
	var i = 0;
	n_syll = 0;
	for (i = 0; i < words.length; i++)
	{
		var n = SyllableCount(words[i]); 
		n_syll += n;
		if ( n >= 3)
		{
			complex.push(words[i]);
		}
	}
	
	return parseFloat(complex.length);
}

function GunningFog()
{
	
	var gf = 0.0;

	if (wc > 0 && sc > 0)
	{
		gf = 0.4* ( (wc / sc) + 100.0 *(cwc/ wc)  );
	}
	var f = Flesch();
	console.log(gf+","+f);
	$("#wc").text(wc.toFixed(0));
	$("#sc").text(sc.toFixed(0));
	$("#cwc").text(cwc.toFixed(0));
	$("#syl_c").text(n_syll.toFixed(0));
	$("#gf").text(gf.toFixed(2));
	$("#fk").text(f.toFixed(2));

	editor.getSession().setAnnotations([{
  row: 0,
  column: 0,
  text: "Strange error",
  type: "warning" // also warning and information
}, {
  row: 1,
  column: 0,
  text: "information",
  type: "information" // also warning and information
}]);

	return gf;
}

function Flesch()
{
	return 206.835 - 1.015* (wc/sc) - 84.6*(n_syll/wc);
}*/


namespace TextEdit
{
    class TextTools
    {
        public int WordCount { get; set; } = 0;
        public int SentenceCount { get; set; } = 0;
        public int ComplexWordCount { get; set; } = 0;
        public int SyllableCount { get; set; } = 0;
        public double GunningFogScore { get; set; } = 0.0;
        public double FleschScore { get; set; } = 0;

        private String _the_text;

        public TextTools()
        {
            _the_text = "";
        }

        public TextTools(String text)
        {
            _the_text = text;
        }

        public void setText(string text)
        {
            _the_text = text;
        }

        public bool analyse()
        {
            WordCount = word_count();
            SentenceCount = sentence_count();
            ComplexWordCount = complex_count();

            if (SentenceCount != 0 && WordCount != 0)
            {
                FleschScore = 206.835 - 1.015 * ((double)WordCount / (double)SentenceCount) - 84.6 * ((double)SyllableCount / (double)WordCount);
                GunningFogScore = 0.4 * (( (double)WordCount / (double)SentenceCount) + 100.0 * ((double)ComplexWordCount /(double) WordCount));
            }
            else
            {
                FleschScore = -1.0;
                GunningFogScore = -1.0;
                return false;
            }


            return true;
        }

        private int word_count()
        {
            return _the_text.Split(' ').Count();
        }

        private int sentence_count()
        {
            return _the_text.Split('.').Count();
        }

        private int syllable_count(string word)
        {
            word = word.ToLower().Trim();
            int count = System.Text.RegularExpressions.Regex.Matches(word, "[aeiouy]+").Count;
            if ((word.EndsWith("e") || (word.EndsWith("es") || word.EndsWith("ed"))) && !word.EndsWith("le"))
                count--;
            return count;
        }

        private int complex_count()
        {
            
            string[] words = _the_text.Split(' ');
            int complex = 0;
            int n_syll = 0;
            SyllableCount = 0;
            foreach (string w in words)
            {
                n_syll = syllable_count(w);
                SyllableCount += n_syll;
                if (n_syll >= 3)
                {
                    complex ++;
                }
            }

            return complex;
        }
        
    }
}
