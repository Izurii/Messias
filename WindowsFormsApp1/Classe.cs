using System;

public class Class1
{
	public Class1()
	{

        round(double x)
        {
            double y = Math.Round((x * 2), MidpointRounding.AwayFromZero);
            x = y / 2;
        }


    }
}
