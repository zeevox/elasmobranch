namespace Elasmobranch
{
    public static class Week2Selection
    {

        public static bool EqualityCheck(int int1, int int2)
        {
            return int1 == int2;
        }

        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public static bool IsLeapYear(int year)
        {
            if (year % 4 != 0) return false;

            if (year % 100 == 0) return year % 400 == 0;

            return true;
        }

        public static double GreatestOfThree(double p1, double p2, double p3)
        {
            if (p1 > p2 && p1 > p3) return p1;
            if (p2 > p1 && p2 > p3) return p2;
            return p3;
        }

        public static string DetermineQuadrant(double x, double y)
        {
            if (x > 0 && y > 0) return "I";
            if (x < 0 && y > 0) return "II";
            if (x < 0 && y < 0) return "III";
            if (x > 0 && y < 0) return "IV";
            if (x == 0 && y == 0) return "origin";
            return "one of the coordinates is zero or my logic is flawed";
        }

        public static string ScoreToGrade(double quiz, double block, double final)
        {
            var average = (quiz + block + final) / 3;
            if (average >= 90) return "A*";
            if (average >= 80) return "A";
            if (average >= 70) return "B";
            return average >= 50 ? "C" : "F";
        }

        public static string Triangulator(int side1, int side2, int side3)
        {
            if (side1 == side2 && side2 == side3) return "equilateral";

            if (side1 == side2 || side2 == side3 || side1 == side3) return "isosceles";
            
            // triangle inequality theorem
            if (side1 + side2 > side3 && side2 + side3 > side1 && side1 + side3 > side2) return "scalene";

            return "impossible";
        }
    }
}