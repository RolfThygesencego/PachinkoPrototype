using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System;

public class CSVWriter
{
    string filename = Application.dataPath + "/test.csv";

    [System.Serializable]
    public class GoalData
    {
        public int GoalScored1;
        public int GoalScored2;
        public int GoalScored3;
        public int GoalScored4;
        public int GoalScored5;
        public int GoalScored6;
        public int GoalScored7;
        public int GoalScored8;
        public int GoalScored9;
        public int TimesSpun;
    }

    [System.Serializable]
    public class GoalDataList
    {
        public GoalData[] GoalData;

    }

    public GoalDataList myGoalData = new GoalDataList();


    private void Update()
    {

    }

    public void CreateCVSGoals()
    {
        TextWriter tw = new StreamWriter(filename, false);
    tw.WriteLine("Goal1, Goal2, Goal 3, Goal 4, Goal 5, Goal 6, Goal 7, Goal 8, Goal 9, Times Spun, Balls Per Spin");
        tw.Close();
    }
    public void WriteCSVSaveGoals(int goal1, int goal2, int goal3, int goal4, int goal5, int goal6, int goal7, int goal8, int goal9, int timesSpun, int ballsPerSpin)
    {


        StreamWriter tw = new StreamWriter(filename, true);
        tw.WriteLine(goal1 + "," +
            goal2 + "," +
            goal3 + "," +
            goal4 + "," +
            goal5 + "," +
            goal6 + "," +
            goal7 + "," +
            goal8 + "," +
            goal9 + "," +
            timesSpun + "," +
            ballsPerSpin);
        tw.Close();

    }

    public int[] loadCVSGoalsScored()
    {
        TextReader tw = new StreamReader(filename);
        var data = tw.ReadToEnd();
        string[] dataValues = data.Split(",");
        int[] goalIndex = new int[dataValues.Length];
        for (int i = 0; i < 9; i++)
        {
            
            int goalValue;
            
            bool succesfulParse = int.TryParse(dataValues[i], out goalValue);
            if (succesfulParse)
            {
                goalIndex[i] = goalValue;
            }
            else
            {
                goalIndex[i] = 0;
                
            }
            
        }
        tw.Close();
        return goalIndex;
    }
    public int LoadCVSTimesSPun()
    {
        TextReader tw = new StreamReader(filename);
        var data = tw.ReadToEnd();
        string[] dataValues = data.Split(",");
        int spinValue;
        bool succesfulParse = int.TryParse(dataValues[dataValues.Length-2], out spinValue);
        if (succesfulParse)
        {

            tw.Close();
            return spinValue;
        }
        else
        {
            tw.Close();
            return 0;
        }

    }
    public int LoadCVSBallsPerSpin()
    {
        TextReader tw = new StreamReader(filename);
        var data = tw.ReadToEnd();
        string[] dataValues = data.Split(",");
        int ballsValue;
        bool succesfulParse = int.TryParse(dataValues[dataValues.Length - 1], out ballsValue);
        if (succesfulParse)
        {

            tw.Close();
            return ballsValue;
        }
        else
        {
            tw.Close();
            return 0;
        }

    }







    //public void WriteCSVSaveGoals(int goal1, int goal2, int goal3, int goal4, int goal5, int goal6, int goal7, int goal8, int goal9, int timesSpun)
    //{
    //    if (myGoalData.GoalData.Length > 0)
    //    {
    //        TextWriter tw = new StreamWriter(filename, false);
    //        tw.WriteLine($"Goal1, Goal2, Goal 3, Goal 4, Goal 5, Goal 6, Goal 7, Goal 8, Goal 9, timesSpun");
    //        tw.Close();

    //        tw = new StreamWriter(filename, true);

    //        for (int i = 0; i < myGoalData.GoalData.Length; i++)
    //        {
    //            tw.WriteLine(myGoalData.GoalData[i].GoalScored1 + "," +
    //                myGoalData.GoalData[i].GoalScored2 + "," +
    //                myGoalData.GoalData[i].GoalScored3 + "," +
    //                myGoalData.GoalData[i].GoalScored4 + "," +
    //                myGoalData.GoalData[i].GoalScored5 + "," +
    //                myGoalData.GoalData[i].GoalScored6 + "," +
    //                myGoalData.GoalData[i].GoalScored7 + "," +
    //                myGoalData.GoalData[i].GoalScored8 + "," +
    //                myGoalData.GoalData[i].GoalScored9 + "," +
    //                myGoalData.GoalData[i].TimesSpun);
    //        }
    //        tw.Close();

    //    }
    //}
}
