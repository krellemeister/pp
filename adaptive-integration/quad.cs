using static System.Math;
using static System.Console;
using System;

public class quadrature {
	public class recAdapt {
		public static double integrate(Func<double, double> f, double a, double b, double sig, double eta) {
			double f2 = f(a + 2*(b-a)/6), f3 = f(a + 4*(b-a)/6); // setup f2 and f3
			return integrator(f, a, b, sig, eta, f2, f3);
		}
		private static double integrator(Func<double, double> f, double a, double b, double sig, double eta, double f2, double f3) {	
			double f1 = f(a + (b-a)/6), f4 = f(a + 5*(b-a)/6); // setup f1 and f4
			double Q = (b-a)*(2*f1 + f2 + f3 + 2*f4)/6; // sum of fs weighted by w and scaled by the interval
			double q = (b-a)*(f1 + f2 + f3 + f4)/4; // sum of fs weighted by v and scaled by the interval
			double err = Abs(Q - q), tol = sig + eta*Abs(Q);
			if (err < tol)
				return Q;
			else 
				return integrator(f, a, (a+b)/2, sig/Sqrt(2), eta, f1, f2) + integrator(f, (a+b)/2, b, sig/Sqrt(2), eta, f3, f4);
		}
	}

	public class varTrans {
		public static double integrate(Func<double, double> f, double a, double b, double sig, double eta) {
			Func<double, double> g = (t) => f((a+b)/2 + (b-a)/2*Cos(t))*Sin(t)*(b-a)/2; // the variable transform with appropriate scaling
			return recAdapt.integrate(g, 0, PI, sig, eta);
		}
	}
}
