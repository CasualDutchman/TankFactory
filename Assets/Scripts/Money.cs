using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Money {

    public float ones;
    public int thousands;
    public int millions;
    public int billions;
    public int trillions;
    public int quadrillions;
    public int quintillions;

    public Money(float o, int t, int m, int b, int tr, int q, int qu) {
        ones = o;
        thousands = t;
        millions = m;
        billions = b;
        trillions = tr;
        quadrillions = q;
        quintillions = qu;
    }

    public Money(float o, int t, int m, int b, int tr, int q) {
        ones = o;
        thousands = t;
        millions = m;
        billions = b;
        trillions = tr;
        quadrillions = q;
        quintillions = 0;
    }

    public Money(float o, int t, int m, int b, int tr) {
        ones = o;
        thousands = t;
        millions = m;
        billions = b;
        trillions = tr;
        quadrillions = 0;
        quintillions = 0;
    }

    public Money(float o, int t, int m, int b) {
        ones = o;
        thousands = t;
        millions = m;
        billions = b;
        trillions = 0;
        quadrillions = 0;
        quintillions = 0;
    }

    public Money(float o, int t, int m) {
        ones = o;
        thousands = t;
        millions = m;
        billions = 0;
        trillions = 0;
        quadrillions = 0;
        quintillions = 0;
    }

    public Money(float o, int t) {
        ones = o;
        thousands = t;
        millions = 0;
        billions = 0;
        trillions = 0;
        quadrillions = 0;
        quintillions = 0;
    }

    public Money(float o) {
        ones = o;
        thousands = 0;
        millions = 0;
        billions = 0;
        trillions = 0;
        quadrillions = 0;
        quintillions = 0;
    }

    public void Add(Money money) {
        ones += money.ones;
        thousands += money.thousands;
        millions += money.millions;
        billions += money.billions;
        trillions += money.trillions;
        quadrillions += money.quadrillions;
        quintillions += money.quintillions;

        if (ones >= 1000) {
            thousands++;
            ones -= 1000;
        }

        if (thousands >= 1000) {
            millions++;
            thousands -= 1000;
        }

        if (millions >= 1000) {
            billions++;
            millions -= 1000;
        }

        if (billions >= 1000) {
            trillions++;
            billions -= 1000;
        }

        if (trillions >= 1000) {
            quadrillions++;
            trillions -= 1000;
        }

        if (quadrillions >= 1000) {
            quintillions++;
            quadrillions -= 1000;
        }

        if (quintillions >= 1000) {
            quintillions = 999;
        }
    }

    public bool Remove(Money money) {

        if (this < money)
            return false;

        ones -= money.ones;
        thousands -= money.thousands;
        millions -= money.millions;
        billions -= money.billions;
        trillions -= money.trillions;
        quadrillions -= money.quadrillions;
        quintillions -= money.quintillions;

        if (ones < 0) {
            if (thousands > 0 || millions > 0 || billions > 0 || trillions > 0 || quadrillions > 0 || quintillions > 0) {
                thousands--;
                ones += 1000;
            }
        }

        if (thousands < 0) {
            if (millions > 0 || billions > 0 || trillions > 0 || quadrillions > 0 || quintillions > 0) {
                millions--;
                thousands += 1000;
            }
        }

        if (millions < 0) {
            if (billions > 0 || trillions > 0 || quadrillions > 0 || quintillions > 0) {
                billions--;
                millions += 1000;
            }
        }

        if (billions < 0) {
            if (trillions > 0 || quadrillions > 0 || quintillions > 0) {
                trillions--;
                billions += 1000;
            }
        }

        if (trillions < 0) {
            if (quadrillions > 0 || quintillions > 0) {
                quadrillions--;
                trillions += 1000;
            }
        }

        if (quadrillions < 0) {
            if (quintillions > 0) {
                quintillions--;
                quadrillions += 1000;
            }
        }

        if (quintillions < 0) {
            quintillions = 0;
        }

        return true;
    }

    public override string ToString() {
        if (quintillions > 0) {
            return quintillions + "." + CorrectNumber(quadrillions) + "." + CorrectNumber(trillions) + "Q";
        }
        if (quadrillions > 0) {
            return quadrillions + "." + CorrectNumber(trillions) + "." + CorrectNumber(billions) + "q";
        }
        if (trillions > 0) {
            return trillions + "." + CorrectNumber(billions) + "." + CorrectNumber(millions) + "t";
        }
        if (billions > 0) {
            return billions + "." + CorrectNumber(millions) + "." + CorrectNumber(thousands) + "b";
        }
        if (millions > 0) {
            return millions + "." + CorrectNumber(thousands) + "." + CorrectNumber(Mathf.FloorToInt(ones)) + "m";
        }
        if (thousands > 0) {
            return thousands + "." + CorrectOnes(ones);//  CorrectNumber(Mathf.FloorToInt(ones)) + float.Parse(ones.ToString().Split('.')[1]);
        }
        return ones.ToString("F2");
    }

    string CorrectNumber(int i) {
        char[] c = i.ToString().ToCharArray();
        if (c.Length == 1)
            return "00" + i;

        if (c.Length == 2)
            return "0" + i;

        return i.ToString();
    }

    string CorrectOnes(float i) {
        string str = i.ToString("F2");
        string s = "";
        string end = "";

        if (str.Contains(".")) {
            s = str.Split('.')[0];
            end = "," + str.Split('.')[1];
        } else {
            s = str;
        }

        char[] c = s.ToCharArray();
        if (c.Length == 1)
            return "00" + s + end;

        if (c.Length == 2)
            return "0" + s + end;

        return str;
    }

    public override bool Equals(object obj) {
        if(obj is Money)
            return this == (Money)obj;
        return false;
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

    public static Money operator +(Money a, Money b) {
        a.Add(b);
        return a;
    }

    public static Money operator -(Money a, Money b) {
        a.Remove(b);
        return a;
    }

    public static bool operator >=(Money a, Money b) {
        if(a.quintillions > 0 || b.quintillions > 0) {
            return a.quintillions >= b.quintillions;
        }
        if (a.quadrillions > 0 || b.quadrillions > 0) {
            return a.quadrillions >= b.quadrillions;
        }
        if (a.trillions > 0 || b.trillions > 0) {
            return a.trillions >= b.trillions;
        }
        if (a.billions > 0 || b.billions > 0) {
            return a.billions >= b.billions;
        }
        if (a.millions > 0 || b.millions > 0) {
            return a.millions >= b.millions;
        }
        return a.thousands >= b.thousands;
    }

    public static bool operator <=(Money a, Money b) {
        if (a.quintillions > 0 || b.quintillions > 0) {
            return a.quintillions <= b.quintillions;
        }
        if (a.quadrillions > 0 || b.quadrillions > 0) {
            return a.quadrillions <= b.quadrillions;
        }
        if (a.trillions > 0 || b.trillions > 0) {
            return a.trillions <= b.trillions;
        }
        if (a.billions > 0 || b.billions > 0) {
            return a.billions <= b.billions;
        }
        if (a.millions > 0 || b.millions > 0) {
            return a.millions <= b.millions;
        }
        return a.thousands <= b.thousands;
    }

    public static bool operator >(Money a, Money b) {
        if (a.quintillions > 0 || b.quintillions > 0) {
            return a.quintillions > b.quintillions;
        }
        if (a.quadrillions > 0 || b.quadrillions > 0) {
            return a.quadrillions > b.quadrillions;
        }
        if (a.trillions > 0 || b.trillions > 0) {
            return a.trillions > b.trillions;
        }
        if (a.billions > 0 || b.billions > 0) {
            return a.billions > b.billions;
        }
        if (a.millions > 0 || b.millions > 0) {
            return a.millions > b.millions;
        }
        return a.thousands > b.thousands;
    }

    public static bool operator <(Money a, Money b) {
        if (a.quintillions > 0 || b.quintillions > 0) {
            return a.quintillions < b.quintillions;
        }
        if (a.quadrillions > 0 || b.quadrillions > 0) {
            return a.quadrillions < b.quadrillions;
        }
        if (a.trillions > 0 || b.trillions > 0) {
            return a.trillions < b.trillions;
        }
        if (a.billions > 0 || b.billions > 0) {
            return a.billions < b.billions;
        }
        if (a.millions > 0 || b.millions > 0) {
            return a.millions < b.millions;
        }
        return a.thousands < b.thousands;
    }

    public static bool operator ==(Money a, Money b) {
        if (a.quintillions > 0 || b.quintillions > 0) {
            return a.quintillions == b.quintillions;
        }
        if (a.quadrillions > 0 || b.quadrillions > 0) {
            return a.quadrillions == b.quadrillions;
        }
        if (a.trillions > 0 || b.trillions > 0) {
            return a.trillions == b.trillions;
        }
        if (a.billions > 0 || b.billions > 0) {
            return a.billions == b.billions;
        }
        if (a.millions > 0 || b.millions > 0) {
            return a.millions == b.millions;
        }
        return a.thousands == b.thousands;
    }

    public static bool operator !=(Money a, Money b) {
        if (a.quintillions > 0 || b.quintillions > 0) {
            return a.quintillions != b.quintillions;
        }
        if (a.quadrillions > 0 || b.quadrillions > 0) {
            return a.quadrillions != b.quadrillions;
        }
        if (a.trillions > 0 || b.trillions > 0) {
            return a.trillions != b.trillions;
        }
        if (a.billions > 0 || b.billions > 0) {
            return a.billions != b.billions;
        }
        if (a.millions > 0 || b.millions > 0) {
            return a.millions != b.millions;
        }
        return a.thousands != b.thousands;
    }
}
