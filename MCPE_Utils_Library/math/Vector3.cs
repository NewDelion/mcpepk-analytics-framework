using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.math
{
    public class Vector3
    {
        public const int SIDE_DOWN = 0;
        public const int SIDE_UP = 1;
        public const int SIDE_NORTH = 2;
        public const int SIDE_SOUTH = 3;
        public const int SIDE_WEST = 4;
        public const int SIDE_EAST = 5;

        public double x;
        public double y;
        public double z;

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
        }

        public Vector3(double x)
        {
            this.x = x;
            this.y = 0;
            this.z = 0;
        }

        public Vector3()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        public double getX()
        {
            return this.x;
        }

        public double getY()
        {
            return this.y;
        }

        public double getZ()
        {
            return this.z;
        }

        public int getFloorX()
        {
            return (int)this.x;
        }

        public int getFloorY()
        {
            return (int)this.y;
        }

        public int getFloorZ()
        {
            return (int)this.z;
        }

        public double getRight()
        {
            return this.x;
        }

        public double getUp()
        {
            return this.y;
        }

        public double getForward()
        {
            return this.z;
        }

        public double getSouth()
        {
            return this.x;
        }

        public double getWest()
        {
            return this.z;
        }

        public Vector3 add(double x, double y, double z)
        {
            return new Vector3(this.x + x, this.y + y, this.z + z);
        }

        public Vector3 add(double x, double y)
        {
            return this.add(x, y, 0);
        }

        public Vector3 add(double x)
        {
            return this.add(x, 0, 0);
        }

        public Vector3 add(Vector3 x)
        {
            return new Vector3(this.x + x.getX(), this.y + x.getY(), this.z + x.getZ());
        }

        public Vector3 subtract(double x, double y, double z)
        {
            return this.add(-x, -y, -z);
        }

        public Vector3 subtract(double x, double y)
        {
            return this.subtract(x, y, 0);
        }

        public Vector3 subtract(double x)
        {
            return this.subtract(x, 0, 0);
        }

        public Vector3 subtract()
        {
            return this.subtract(0, 0, 0);
        }

        public Vector3 subtract(Vector3 x)
        {
            return this.add(-x.getX(), -x.getY(), -x.getZ());
        }

        public Vector3 multiply(double number)
        {
            return new Vector3(this.x * number, this.y * number, this.z * number);
        }

        public Vector3 divide(double number)
        {
            return new Vector3(this.x / number, this.y / number, this.z / number);
        }

        public Vector3 ceil()
        {
            return new Vector3((int)Math.Ceiling(this.x), (int)Math.Ceiling(this.y), (int)Math.Ceiling(this.z));
        }

        public Vector3 floor()
        {
            return new Vector3(this.getFloorX(), this.getFloorY(), this.getFloorZ());
        }

        public Vector3 round()
        {
            return new Vector3(Math.Round(this.x), Math.Round(this.y), Math.Round(this.z));
        }

        public Vector3 abs()
        {
            return new Vector3((int)Math.Abs(this.x), (int)Math.Abs(this.y), (int)Math.Abs(this.z));
        }

        public Vector3 getSide(int side, int step)
        {
            switch (side)
            {
                case Vector3.SIDE_DOWN:
                {
                    return new Vector3(this.x, this.y - step, this.z);
                }
                case Vector3.SIDE_UP:
                {
                    return new Vector3(this.x, this.y + step, this.z);
                }
                case Vector3.SIDE_NORTH:
                {
                    return new Vector3(this.x, this.y, this.z - step);
                }
                case Vector3.SIDE_SOUTH:
                {
                    return new Vector3(this.x, this.y, this.z + step);
                }
                case Vector3.SIDE_WEST:
                {
                    return new Vector3(this.x - step, this.y, this.z);
                }
                case Vector3.SIDE_EAST:
                {
                    return new Vector3(this.x + step, this.y, this.z);
                }
                default:
                {
                    return this;
                }
            }
        }

        public Vector3 getSide(int side)
        {
            return this.getSide(side, 1);
        }

        public static int getOppositeSide(int side)
        {
            switch (side)
            {
                case Vector3.SIDE_DOWN:
                {
                    return Vector3.SIDE_UP;
                }
                case Vector3.SIDE_UP:
                {
                    return Vector3.SIDE_DOWN;
                }
                case Vector3.SIDE_NORTH:
                {
                    return Vector3.SIDE_SOUTH;
                }
                case Vector3.SIDE_SOUTH:
                {
                    return Vector3.SIDE_NORTH;
                }
                case Vector3.SIDE_WEST:
                {
                    return Vector3.SIDE_EAST;
                }
                case Vector3.SIDE_EAST:
                {
                    return Vector3.SIDE_WEST;
                }
                default:
                {
                    return -1;
                }
            }
        }

        public double distanceSquared(Vector3 pos)
        {
            return Math.Pow(this.x - pos.x, 2) + Math.Pow(this.y - pos.y, 2) + Math.Pow(this.z - pos.z, 2);
        }

        public double distance(Vector3 pos)
        {
            return Math.Sqrt(this.distanceSquared(pos));
        }

        public double maxPlainDistance(double x, double z)
        {
            return Math.Max(Math.Abs(this.x - x), Math.Abs(this.z - z));
        }

        public double maxPlainDistance(double x)
        {
            return this.maxPlainDistance(x, 0);
        }

        public double maxPlainDistance()
        {
            return this.maxPlainDistance(0, 0);
        }

        public double maxPlainDistance(Vector2 vector)
        {
            return this.maxPlainDistance(vector.x, vector.y);
        }

        public double maxPlainDistance(Vector3 x)
        {
            return this.maxPlainDistance(x.x, x.z);
        }

        public double lengthSquared()
        {
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }

        public double length()
        {
            return Math.Sqrt(this.lengthSquared());
        }

        public Vector3 normalize()
        {
            double len = this.lengthSquared();
            if (len > 0)
                return this.divide(Math.Sqrt(len));
            return new Vector3(0, 0, 0);
        }

        public double dot(Vector3 v)
        {
            return this.x * v.x + this.y * v.y + this.z * v.z;
        }

        public Vector3 cross(Vector3 v)
        {
            return new math.Vector3(
                this.y * v.z - this.z * v.y,
                this.z * v.x - this.x * v.z,
                this.x * v.y - this.y * v.x
            );
        }

        public Vector3 geetIntermediateWithXValue(Vector3 v, double x)
        {
            double xDiff = v.x - this.x;
            double yDiff = v.y - this.y;
            double zDiff = v.z - this.z;
            if (xDiff * xDiff < 0.0000001)
                return null;
            double f = (x - this.x) / xDiff;
            if (f < 0 || f > 1)
                return null;
            return new Vector3(this.x + xDiff * f, this.y + yDiff * f, this.z + zDiff * f);
        }

        public Vector3 geetIntermediateWithYValue(Vector3 v, double y)
        {
            double xDiff = v.x - this.x;
            double yDiff = v.y - this.y;
            double zDiff = v.z - this.z;
            if (yDiff * yDiff < 0.0000001)
                return null;
            double f = (y - this.y) / yDiff;
            if (f < 0 || f > 1)
                return null;
            return new Vector3(this.x + xDiff * f, this.y + yDiff * f, this.z + zDiff * f);
        }

        public Vector3 geetIntermediateWithZValue(Vector3 v, double z)
        {
            double xDiff = v.x - this.x;
            double yDiff = v.y - this.y;
            double zDiff = v.z - this.z;
            if (zDiff * zDiff < 0.0000001)
                return null;
            double f = (z - this.z) / zDiff;
            if (f < 0 || f > 1)
                return null;
            return new Vector3(this.x + xDiff * f, this.y + yDiff * f, this.z + zDiff * f);
        }

        public Vector3 setComponents(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            return this;
        }

        public override string ToString()
        {
            return string.Format("Vector3(x={0}, y={1}, z={2})", this.x, this.y, this.z);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector3))
                return false;
            Vector3 other = (Vector3)obj;
            return this.x == other.x && this.y == other.y && this.z == other.z;
        }
    }
}
