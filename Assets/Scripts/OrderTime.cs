using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OrderTime {

    int days;
    int hours;
    int minutes;
    int seconds;

    public OrderTime(double time) {
        days = (int)(time / (60 * 60 * 24));
        hours = (int)(time / (60 * 60)) % 24;
        minutes = (int)(time / 60) % 60;
        seconds = (int)(time % 60);
    }

    public OrderTime(int d, int h, int m, int s) {
        days = d;
        hours = h;
        minutes = m;
        seconds = s;
    }

    public int Days { get { return days; } }

    public int Hours { get { return hours; } }

    public int Minutes { get { return minutes; } }

    public int Seconds { get { return seconds; } }

    public void RemoveSecond() {
        seconds--;
        if (seconds < 0) {
            seconds = 59;
            minutes--;
        }
        if (minutes < 0) {
            minutes = 59;
            hours--;
        }
        if (hours < 0) {
            hours = 59;
            days--;
        }
    }

    public string DaysToCode() {
        return days + "/" + hours + "/" + minutes + "/" + seconds + "";
    }

    public void CodeToDays(string code) {
        string[] strList = code.Split('/');
        days = int.Parse(strList[0]);
        hours = int.Parse(strList[1]);
        minutes = int.Parse(strList[2]);
        seconds = int.Parse(strList[3]);
    }

    public bool HasTime() {
        return seconds > 0 || minutes > 0 || hours > 0 || days > 0;
    }

    public float Percent(OrderTime time) {
        double current = this.ToDouble();
        double other = time.ToDouble();

        return (float)(current / other);
    }

    public double ToDouble() {
        double d = 0;

        d += days * (60 * 60 * 24);
        d += hours * (60 * 60);
        d += minutes * 60;
        d += seconds;

        return d;
    }

    public string GetTime() {
        string str = "";

        int days = Days;
        int hours = Hours;
        int minutes = Minutes;
        int seconds = Seconds;

        if (days > 0)
            str += days + "d: ";

        if (days > 0 || hours > 0)
            str += hours + "h: ";

        if (days > 0 || hours > 0 || minutes > 0)
            str += minutes + "m: ";

        str += seconds + "s";

        return str;
    }
}
