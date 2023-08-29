using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class PyramidLayout : ObLayout
{
    public override void SetupObstacles()
    {
        int amountOfObsPerRow = 1;
        for (int i = 1; i < ObstacleRows + 1; i++)
        {

            int sideDistance = 1;
            float offset = ((LengthDistance / 2) * i);

            for (int j = 1; j < amountOfObsPerRow; j += 1)
            {

                GameObject ObstacleLeft = Object.Instantiate(PrefabHolder.Instance.ObstacleNoRand, new Vector2(0, 0), Quaternion.identity);
                ObstacleLeft.transform.position = new Vector2(sideDistance * LengthDistance - offset, -i * LengthDistance);


                ObstacleLeft.transform.SetParent(obstacleHolder.transform, false);

                //ObstacleLeft.transform.position = new Vector2(ObstacleLeft.transform.position.x + LengthDistance / 2, ObstacleLeft.transform.position.y);
                if (i % 2 == 0)
                {


                }
                obstacles.Add(ObstacleLeft);


                sideDistance += 1;
                if (ObstacleRows == amountOfObsPerRow)
                {
                    bottomObs.Add(ObstacleLeft);
                }

            }

            amountOfObsPerRow += 1;
        }
    }
}

