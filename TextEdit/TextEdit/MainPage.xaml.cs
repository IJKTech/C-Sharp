using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TextEdit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private TextTools tools;

        public MainPage()
        {
            this.InitializeComponent();
            this.tools = new TextTools();
        }

        private void rebText_TextChanged(object sender, RoutedEventArgs e)
        {
            RichEditBox theBox = (RichEditBox)sender;
            if (null != theBox)
            {
                String text;
                theBox.Document.GetText(Windows.UI.Text.TextGetOptions.None, out text);
                tools.setText(text);
                tools.analyse();
                tbWordCount.Text = tools.WordCount.ToString(); 
                tbGunningFog.Text =  tools.GunningFogScore.ToString();
                tbFlesch.Text = tools.FleschScore.ToString();
                tbComplexWords.Text = tools.ComplexWordCount.ToString();
                tbSyllables.Text = tools.SyllableCount.ToString();
            }
        }
    }
}
