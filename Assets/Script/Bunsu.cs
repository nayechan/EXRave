using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunsu
{
    public int a;
    public int b;


    public Bunsu(int a, int b)
    {
        this.a = a;
        this.b = b;
    }
    public void giyak()
    {
        int aa = a, bb = b;
        int r = 1;
        
        while (bb != 0)
        {
            r = aa % bb;
            aa = bb;
            bb = r;
        }
        if(aa!=0)a /= aa;
        if(aa!=0)b /= aa;
    }

    public override string ToString()
    {
        return a+"/"+b;
    }

    public static bool operator >(Bunsu A, Bunsu B)
    {
        if (A.a * B.b > A.b * B.a) return true;
        else return false;
    }

    public static bool operator <(Bunsu A, Bunsu B)
    {
        if (A.a * B.b < A.b * B.a) return true;
        else return false;
    }

    public static bool operator >=(Bunsu A, Bunsu B)
    {
        if (A.a * B.b >= A.b * B.a) return true;
        else return false;
    }

    public static bool operator <=(Bunsu A, Bunsu B)
    {
        if (A.a * B.b <= A.b * B.a) return true;
        else return false;
    }

    public static explicit operator float(Bunsu v)
    {
        return (float)v.a / v.b;
    }

    public static bool operator ==(Bunsu A, Bunsu B)
    {
        if (A.a * B.b == B.a * A.b)
        {
            return true;
        }
        else return false;
    }

    public static bool operator !=(Bunsu A, Bunsu B)
    {
        if (A.a * B.b == B.a * A.b)
        {
            return false;
        }
        else return true;
    }

    bool isPositive()
    {
        if (a > 0 && b > 0) return true;
        if (a > 0 && b < 0) return false;
        if (a < 0 && b > 0) return false;
        if (a < 0 && b < 0) return true;
        else return false;
    }

    bool isZero()
    {
        if (a == 0) return true;
        else return false;
    }

    bool isNAN()
    {
        if (b == 0) return true;
        else return false;
    }

    public static Bunsu operator +(Bunsu A, Bunsu B)
    {
        Bunsu x = new Bunsu((A.a * B.b) + (A.b * B.a), (A.b * B.b));
        x.giyak();
        return x;
    }

    public static Bunsu operator *(Bunsu A, Bunsu B)
    {
        Bunsu x = new Bunsu(A.a * B.a, A.b * B.b);
        x.giyak();
        return x;
    }

    public static Bunsu operator /(Bunsu A, Bunsu B)
    {
        Bunsu x = new Bunsu(A.a * B.b, A.b * B.a);
        x.giyak();
        return x;
    }

    public static Bunsu operator -(Bunsu A)
    {
        Bunsu x = new Bunsu(-A.a, A.b);
        x.giyak();
        return x;
    }

    public static Bunsu operator -(Bunsu A, Bunsu B)
    {
        Bunsu x = A + (-B);
        x.giyak();
        return x;
    }

    public static Bunsu operator %(Bunsu A, Bunsu B)
    {
        while(A.isPositive() || A.isZero())
        {
            A -= B;
        }
        A += B;
        A.giyak();
        return A;
        
    }
    public override bool Equals(object o)
    {
        Bunsu bb = (Bunsu)o;
        if (((float)a / b) == ((float)bb.a / bb.b))
        {
            return true;
        }
        else return false;
    }

    public override int GetHashCode()
    {
        return a + b;
    }

}
