using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Runtime.Serialization;

using System.Collections;
using System.Xml;

using System;
using System.Collections;
using System.Text;
//using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using Zahrada.PomocneTridy;

// toto je slepa ulicka ....
namespace Zahrada.PomocneTridy
{
    [DataContract]    
    public class BindableBrush
    {
        private string _brushName;
        /// <summary>
        /// Initializes a new instance of the BindableBrush class.
        /// </summary>
        public BindableBrush( Color c, string name)
        {
            Color = c;
            _brushName = name;

        }

        [DataMember]
        public Color Color
        { get; set; }


        public SolidColorBrush Brush
        {
            get
            {
                return new SolidColorBrush(Color);
            }
            set
            {
                Color = value.Color;
            }
        }

        [DataMember]
        public string BrushName
        {
            get
            {
                return _brushName;
            }
            set
            {
                _brushName = value;
            }
        }
    }
}