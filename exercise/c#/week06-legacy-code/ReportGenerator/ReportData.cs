﻿namespace ReportGenerator
{
    public sealed class ReportData(int id, double value, string description)
    {
        public int Id => id;

        public double Value => value;

        public string Description => description;
    }
}