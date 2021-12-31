using System;
using Collection;
namespace NumericalAnalysis.Cluster
{
	public class LBG
	{
		public int K;
		public List<List<double>> Datas;
		public List<int> Sets;
		public List<List<double>> Vectors;
		public double D;
		public int N;
		public int NMax;
		public double Epislon;
		public LBG(int k, int nmax, double epsilon)
		{
			K = k;
			Datas = new();
			Sets = new();
			Vectors = new();
			NMax = nmax;
			Epislon = epsilon;
			N = 1;
		}
        public void RegisterDatas(params List<double>[] datas) => Datas.AddRange(datas);
        public void Add(List<double> a, List<double> b)
		{
			for (int i = 0; i < K; i++)
				a.Values[i] += b.Values[i];
		}
		public double Distance(List<double> a, List<double> b)
		{
			double s = 0.0;
			for (int i = 0; i < K; i++)
				s += (a.Values[i] - b.Values[i]) * (a.Values[i] - b.Values[i]);
			return s;
		}
		public void Run(Action<List<List<double>>, double> callback)
		{
            List<List<double>> vectors = new();
			vectors.Add(new List<double>(new double[K]));
			List<int> counts = new();
			counts.Add(0);
			foreach (List<double> data in Datas)
			{
				if (data.Length != K)
					throw new Exception();
				Sets.Add(0);
				Add(vectors[0], data);
				counts[0]++;
			}
			for (int k = 0; k < N; k++)
				for (int j2 = 0; j2 < K; j2++)
					vectors[k].Values[j2] /= counts[k];
			D = 0.0;
			for (int l = 0; l < Datas.Length; l++)
				D += Distance(vectors[Sets[l]], Datas[l]);
			D /= K * Datas.Length;
			callback(vectors, D);
			while (true)
			{
				Vectors = new List<List<double>>();
				counts = new List<int>();
				if (N >= NMax)
					foreach (List<double> item in vectors)
					{
						Vectors.Add(item);
						counts.Add(0);
					}
				else
				{
					for (int i4 = 0; i4 < N; i4++)
					{
						List<double> a = new();
						List<double> b = new();
						for (int j3 = 0; j3 < K; j3++)
						{
							a.Add((1.0 + Epislon) * vectors[i4][j3]);
							b.Add((1.0 - Epislon) * vectors[i4][j3]);
						}
						Vectors.Add(a);
						counts.Add(0);
						Vectors.Add(b);
						counts.Add(0);
					}
				}
				N = Vectors.Length;
				for (int i3 = 0; i3 < Datas.Length; i3++)
				{
					double d = Distance(Vectors[Sets[i3] = 0], Datas[i3]);
					for (int j4 = 0; j4 < N; j4++)
					{
						double dd = Distance(Vectors[j4], Datas[i3]);
						if (dd < d)
						{
							d = dd;
							Sets[i3] = j4;
						}
					}
					counts[Sets[i3]]++;
				}
				vectors = new List<List<double>>();
				for (int i2 = 0; i2 < N; i2++)
					vectors.Add(new List<double>(new double[K]));
				for (int n = 0; n < Datas.Length; n++)
					Add(vectors[Sets[n]], Datas[n]);
				for (int m = 0; m < N; m++)
					if (counts[m] != 0)
						for (int j5 = 0; j5 < K; j5++)
							vectors[m].Values[j5] /= counts[m];
					else
						vectors[m] = null;
				double nD = 0.0;
				for (int j = 0; j < Datas.Length; j++)
					nD += Distance(vectors[Sets[j]], Datas[j]);
				nD /= K * Datas.Length;
                List<List<double>> _vectors = new();
				for (int i = 0; i < N; i++)
					if (counts[i] != 0)
						_vectors.Add(vectors[i]);
				vectors = _vectors;
				N = vectors.Length;
				callback(vectors, nD);
				if (Math.Abs((nD - D) / D) < Epislon)
					break;
				D = nD;
			}
		}
	}
}
