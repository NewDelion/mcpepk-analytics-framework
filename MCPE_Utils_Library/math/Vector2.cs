using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.math
{
    public class Vector2
    {
        public double x;
        public double y;

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(double x)
        {
            this.x = x;
            this.y = 0;
        }

        public Vector2()
        {
            this.x = 0;
            this.y = 0;
        }

        public double getX()
        {
            return this.x;
        }

        public double getY()
        {
            return this.y;
        }

        public int getFloorX()
        {
            return (int)this.x;
        }

        public int getFloorY()
        {
            return (int)this.y;
        }

        public Vector2 add(double x, double y)
        {
            return new Vector2(this.x + x, this.y + y);
        }

        public Vector2 add(double x)
        {
            return this.add(x, 0);
        }

        public Vector2 add(Vector2 x)
        {
            return this.add(x.getX(), x.getY());
        }

        public Vector2 subtract(double x, double y)
        {
            return this.add(-x, -y);
        }

        public Vector2 subtract(double x)
        {
            return this.subtract(x, 0);
        }

        public Vector2 subtract(Vector2 x)
        {
            return this.add(-x.getX(), -x.getY());
        }

        public Vector2 ceil()
        {
            return new Vector2((int)(this.x + 1), (int)(this.y + 1));
        }

        public Vector2 floor()
        {
            return new Vector2((int)this.x, (int)this.y);
        }

        public Vector2 round()
        {
            return new Vector2(Math.Round(this.x), Math.Round(this.y));
        }

        public Vector2 abs()
        {
            return new Vector2(Math.Abs(this.x), Math.Abs(this.y));
        }

        public Vector2 multiply(double number)
        {
            return new Vector2(this.x * number, this.y * number);
        }

        public Vector2 divide(double number)
        {
            return new Vector2(this.x / number, this.y / number);
        }

        public double distanceSquared(double x, double y)
        {
            return Math.Pow(this.x - x, 2) + Math.Pow(this.y - y, 2);
        }

        public double distanceSquared(double x)
        {
            return this.distanceSquared(x, 0);
        }

        public double distanceSquared(Vector2 vector)
        {
            return this.distanceSquared(vector.getX(), vector.getY());
        }

        public double distance(double x, double y)
        {
            return Math.Sqrt(this.distanceSquared(x, y));
        }

        public double distance(double x)
        {
            return this.distance(x, 0);
        }

        public double distance(Vector2 vector)
        {
            return Math.Sqrt(this.distanceSquared(vector.getX(), vector.getY()));
        }

        public double lengthSquared()
        {
            return this.x * this.x + this.y * this.y;
        }

        public double length()
        {
            return Math.Sqrt(this.lengthSquared());
        }

        public Vector2 normalize()
        {
            double len = this.lengthSquared();
            if (len != 0)
                return this.divide(Math.Sqrt(len));
            return new Vector2(0, 0);
        }

        public double dot(Vector2 v)
        {
            return this.x * v.x + this.y * v.y;
        }

        public override string ToString()
        {
            return "Vector2(x=" + this.x.ToString() + ", y=" + this.y.ToString() + ")";
        }
    }
}
