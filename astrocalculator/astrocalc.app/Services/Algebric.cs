using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astrocalc.app.services.algebric {
    public static class Algebric {
        public static double Radians(double angle) {
            return (Math.PI / 180) * angle;
        }
        public static double Degrees(double angle) {
            return angle * 180 / Math.PI;
        }
        public static double Cosine(double angle_deg) {
            return Math.Cos(Radians(angle_deg));
        }
        public static double Sine(double angle_deg) {
            return Math.Sin(Radians(angle_deg));
        }
        public static double Tangent(double angle_deg) {
            return Math.Tan(Radians(angle_deg));
        }
        public static double TangentInv(double value) {
            return Degrees(Math.Atan(value));
        }
        public static double SineInv(double value) {
            return Degrees(Math.Asin(value));
        }
        public static double CosineInv(double value) {
            return Degrees(Math.Acos(value));
        }
    }
}
