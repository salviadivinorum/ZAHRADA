using System;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;

namespace Zahrada.PomocneTridy
{
    /// <summary>
    /// Pomocna trida FileLocationEditor pro vyber cesty k souboru v PropertyGridu
    /// </summary>    
    public class FileLocationEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Vyber texturu";
                ofd.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    return ofd.FileName;
                }
            }
            return value;
        }
    }
}
