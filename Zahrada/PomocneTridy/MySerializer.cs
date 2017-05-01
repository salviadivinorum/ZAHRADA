using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;


namespace Zahrada.PomocneTridy
{
    /// <summary>
    /// Pomocna trida s metodou SerializeToString() pro serializaci Listu do Stringu. Pouzivam pres this object, nemusim tvorit tak instanci tridy.
    /// Timto zpusobem si kontroluju na vystupu zda-li se List zmenil. A odstranil jsem tak otravne dialogy typu Ulozit projekt Ano/Ne.
    /// </summary>
    public static class MySerializer
    {
       
        public static string SerializeToString(this object o)
        {
            try
            {
                if (!o.GetType().IsSerializable)
                {
                    return null;
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    new BinaryFormatter().Serialize(stream, o);
                    return Convert.ToBase64String(stream.ToArray());
                }
            }
            catch (Exception e )
            {
                // MessageBox.Show("Projekt nebyl uložen !", "Uložení selhalo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // MessageBox.Show("Výjimka:" + e.ToString(), "Save error:");
                return null;

            }
        }
    }
}
