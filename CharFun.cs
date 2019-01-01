using System;
using System.Collections;
using System.IO;
using System.Text;

namespace CharFun
{
    class MainClass
    {
		/*
		public static string conv(string s)
		{
			byte[] bytes = Encoding.GetEncoding(850).GetBytes(s);
			string _s=Encoding.GetEncoding(932).GetString(bytes);
			return _s;
		}
        */
        public static string conv_var(string s,int a,int b)
		{
			byte[] bytes = Encoding.GetEncoding(a).GetBytes(s);
            string _s = Encoding.GetEncoding(b).GetString(bytes);
            return _s;
		}
		public static string conv_var(string s, Encoding A, Encoding B)
        {
            byte[] bytes = A.GetBytes(s);
            string _s = B.GetString(bytes);
            return _s;
        }
		public static void conv_search_ei(string s,string pattern)
		{
			ArrayList CodePageList = new ArrayList();
			EncodingInfo[] EI = Encoding.GetEncodings();
			int size = EI.Length;
			for (int i = 0; i < size;++i)
			{
				for (int j = 0; j < size;++j)
				{
					if (i == j) continue;
					else
					{
						try
						{
							Encoding A = EI[i].GetEncoding();
							Encoding B = EI[j].GetEncoding();
							string conv = conv_var(s, A, B);
                            if (String.IsNullOrWhiteSpace(pattern))
                            {
                                Console.WriteLine("[count] A: {0} ({1}), B: {2} ({3}), conv: {4}", A.EncodingName, A.WindowsCodePage, B.EncodingName, B.WindowsCodePage, conv);
                            }
                            else
                            {
                                if (conv.Contains(pattern)) Console.WriteLine("[count] A: {0} ({1}), B: {2} ({3}), conv: {4}", A.EncodingName, A.WindowsCodePage, B.EncodingName, B.WindowsCodePage, conv);
                            }
						}
						catch(Exception E)
						{
							//Console.WriteLine(E.Message);
						}
					}
				}
			}
		}
		public static void conv_search_count(string s,string pattern)
        {
            ArrayList CodePageList = new ArrayList();
			ArrayList LogList = new ArrayList();
            int limit = 2000;
			int lastp = int.MinValue;
            for (int i = 0; i < limit; ++i)
            {
				double d = (double)(i + 1) / (double)limit;
				double p = d * 100;
				int ip = (int)Math.Floor(p);
				if (ip != lastp)
				{
					Console.WriteLine("{0}%", ip);
					lastp = ip;
				}
                for (int j = 0; j < limit; ++j)
                {
                    if (i == j) continue;
                    else
                    {
						try
						{
							Encoding A = Encoding.GetEncoding(i);
							Encoding B = Encoding.GetEncoding(j);
							string conv = conv_var(s, A, B);
							if (String.IsNullOrWhiteSpace(pattern))
							{
								LogList.Add(String.Format("[count] A: {0} ({1}), B: {2} ({3}), conv: {4}", A.EncodingName, A.WindowsCodePage, B.EncodingName, B.WindowsCodePage, conv));
							}
							else
							{
								if(conv.Contains(pattern)) LogList.Add(String.Format("[count] A: {0} ({1}), B: {2} ({3}), conv: {4}", A.EncodingName, A.WindowsCodePage, B.EncodingName, B.WindowsCodePage, conv));
							}
						}
						catch(Exception E)
						{
							//Console.WriteLine(E.Message);
						}
					}
                }
            }
			foreach (string log in LogList)
            {
                Console.WriteLine(log);
            }
        }
        public static void Main(string[] args)
        {
			File.WriteAllText("error.log", DateTime.UtcNow.ToString());
			string InputPath = String.Format("{0}//{1}", Directory.GetCurrentDirectory(), "input");
			var F=Directory.GetFiles(InputPath);
            foreach(string filepath in F)
			{
				FileInfo file = new FileInfo(filepath);
				var n = file.Name;
			    string n_ext = Path.GetExtension(n);
				string n_basic = Path.GetFileNameWithoutExtension(n);
				conv_search_count(n_basic, @"あ");
			}
			Console.WriteLine("Nya~");
			Console.WriteLine("Press any key...");
			Console.ReadKey();
        }
    }
}
