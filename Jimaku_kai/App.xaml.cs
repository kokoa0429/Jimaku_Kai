using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Jimaku_kai
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public static int Layout;
        public static int Length;
        public static int Gap;
        public static bool OverWrite;

        [STAThread]
        public static void Main(string[] args)
        {

            LoadConfig();

            if (args.Length != 1)
            {
                App app = new App();
                app.InitializeComponent();
                app.Run();
            }
            else
            {
                MakeFile(args[0]);
            }
        }
        private static void MakeFile(string arg)
        {
                var list = new List<string>();
            using (var sr = new StreamReader(arg))
            {
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
            }
                string[] str = list.ToArray<string>();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("[exedit]");
                sb.AppendLine("width=100");
                sb.AppendLine("height=100");
                sb.AppendLine("rate=60");
                sb.AppendLine("scale=1");
                sb.AppendLine("length=" + (str.Length * Length + 5));
                sb.AppendLine("audio_rate=44100");
                sb.AppendLine("audio_ch=2");

                for (int i = 0; i < str.Length; i++)
                {
                    sb.AppendLine("[" + i + "]");
                    if (Layout == 0)
                    {
                        sb.AppendLine("start=" + (i * Length + 5));
                        sb.AppendLine("end=" + ((i + 1) * Length - 5));
                        sb.AppendLine("layer=1");
                    }
                    else if (Layout == 1)
                    {
                        sb.AppendLine("start=1");
                        sb.AppendLine("end=" + Length);
                        sb.AppendLine("layer=" + (i + 1));
                    }

                    sb.AppendLine("overlay=1");
                    sb.AppendLine("camera=0");
                    sb.AppendLine("[" + i + ".0]");
                    sb.AppendLine("_name=テキスト");
                    sb.AppendLine("サイズ=34");
                    sb.AppendLine("表示速度=0.0");
                    sb.AppendLine("文字毎に個別オブジェクト=0");
                    sb.AppendLine("移動座標上に表示する=0");
                    sb.AppendLine("自動スクロール=0");
                    sb.AppendLine("B=0");
                    sb.AppendLine("I=0");
                    sb.AppendLine("type=0");
                    sb.AppendLine("autoadjust=0");
                    sb.AppendLine("soft=1");
                    sb.AppendLine("monospace=0");
                    sb.AppendLine("align=0");
                    sb.AppendLine("spacing_x=0");
                    sb.AppendLine("spacing_y=0");
                    sb.AppendLine("precision=1");
                    sb.AppendLine("color=ffffff");
                    sb.AppendLine("color2=000000");
                    sb.AppendLine("font=MS UI Gothic");
                    string tmp = "text=";
                    System.Text.Encoding src = System.Text.Encoding.GetEncoding("UTF-16");
                    string unchi = str[i];
                    string textTmp = "";
                    for (int a = 0; a < unchi.Length; a++)
                    {
                        byte[] tempp = src.GetBytes(unchi.Substring(a, 1));
                        textTmp += BitConverter.ToString(tempp).Replace("-", string.Empty).PadRight(4, '0');
                    }
                    tmp += textTmp.PadRight(4096, '0');
                    sb.AppendLine(tmp);
                    sb.AppendLine("[" + i + ".1]");
                    sb.AppendLine("_name=標準描画");
                    sb.AppendLine("X=0.0");
                    sb.AppendLine("Y=0.0");
                    sb.AppendLine("Z=0.0");
                    sb.AppendLine("拡大率=100.00");
                    sb.AppendLine("透明度=0.0");
                    sb.AppendLine("回転=0.00");
                    sb.AppendLine("blend=0");
                }
                Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
                using (var sw = new StreamWriter(Path.GetFileNameWithoutExtension(arg) + ".exo", OverWrite, sjisEnc))
                {
                    sw.Write(sb.ToString());
                    sw.Close();
                }
            
        }

        public static void LoadConfig()
        {
            int i = 0;
            switch (System.Configuration.ConfigurationManager.AppSettings["Layout"])
            {
                case "横":
                    i = 0;
                    break;
                case "縦":
                    i = 1;
                    break;
                default:
                    i = 0;
                    break;
            }
            Layout = i;

            int L = int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Length"], out L) ? L : 125;
            Length = L;

            int G = int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Gap"], out G) ? G : 5;
            Gap = G;

            bool O = bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["OverWrite"], out O) ? O : true;
            OverWrite = O;
        }

        public static void SaveConfig(string Layout,string Length,string Gap,string OverWrite)
        {
            System.Configuration.Configuration config =
                System.Configuration.ConfigurationManager.OpenExeConfiguration(
                System.Configuration.ConfigurationUserLevel.None);
       
            config.AppSettings.Settings["Layout"].Value = Layout;
            config.AppSettings.Settings["Length"].Value = Length;
            config.AppSettings.Settings["Gap"].Value = Gap;
            config.AppSettings.Settings["OverWrite"].Value = OverWrite;

            config.Save();

        }

    }
}
