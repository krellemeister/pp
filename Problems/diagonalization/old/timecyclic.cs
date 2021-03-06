using static System.Console;
using static System.Math;
using static jacobi;
using System.Diagnostics; 
using System;

public class main {
	public static void Main(string[] args) {
		//Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
		int max = 300; // max n
		int step = 10;
		double scale = 7.0/135000; // 300^3*scale = 1400
		Stopwatch timer = new Stopwatch();
		WriteLine(string.Format("# {0, -6} {1, -8} {2, -8}", "n", "cyclic", "n^3"));
		for (int n = 10; n < max; n += step) {
			matrix A = GenRandSymMatrix(n, n);
			timer.Start();
			cyclic(A);
			timer.Stop();

			WriteLine($"{n, -8} {timer.ElapsedMilliseconds, -8:F3} {Pow(n, 3)*scale, -8:F3}");
			timer.Reset();
		}
	}

	private static matrix GenRandSymMatrix(int n, int m) {
		var rand = new Random();
		matrix M = new matrix(n, m);
		double d;
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++) {
				d = 10.0*rand.NextDouble();
				M[i, j] = d;
				M[j, i] = d;
			}
		}
		return M;
	}
}
