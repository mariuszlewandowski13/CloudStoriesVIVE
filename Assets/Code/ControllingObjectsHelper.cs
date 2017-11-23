using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllingObjectsHelper  {

    public static Vector3 CalculatePosSnap(Vector3 vectorToSnap, float snap)
    {

        float minX;
        float maxX = (int)(vectorToSnap.x - 1.0f);

        float minY;
        float maxY = (int)(vectorToSnap.y - 1.0f);

        float minZ;
        float maxZ = (int)(vectorToSnap.z - 1.0f);

        while (maxX < vectorToSnap.x)
        {
            maxX += snap;
        }
        minX = maxX - snap;

        while (maxY < vectorToSnap.y)
        {
            maxY += snap;
        }
        minY = maxY - snap;

        while (maxZ < vectorToSnap.z)
        {
            maxZ += snap;
        }
        minZ = maxZ - snap;

        vectorToSnap.x = ((maxX - vectorToSnap.x) < (vectorToSnap.x - minX) ? maxX : minX);
        vectorToSnap.y = ((maxY - vectorToSnap.y) < (vectorToSnap.y - minY) ? maxY : minY);
        vectorToSnap.z = ((maxZ - vectorToSnap.z) < (vectorToSnap.z - minZ) ? maxZ : minZ);
        return vectorToSnap;
    }

    public static  Vector3 CalculateRotSnap(Vector3 vectorToSnap, float snap)
    {
        if (vectorToSnap.x < 0.0f) vectorToSnap.x += 360.0f;
        if (vectorToSnap.y < 0.0f) vectorToSnap.y += 360.0f;
        if (vectorToSnap.z < 0.0f) vectorToSnap.z += 360.0f;



        float minX = ((int)vectorToSnap.x / (int)snap) * snap;
        float maxX = minX + snap;

        float minY = ((int)vectorToSnap.y / (int)snap) * snap;
        float maxY = minY + snap;

        float minZ = ((int)vectorToSnap.z / (int)snap) * snap;
        float maxZ = minZ + snap;


        vectorToSnap.x = ((maxX - vectorToSnap.x) < (vectorToSnap.x - minX) ? maxX : minX);
        vectorToSnap.y = ((maxY - vectorToSnap.y) < (vectorToSnap.y - minY) ? maxY : minY);
        vectorToSnap.z = ((maxZ - vectorToSnap.z) < (vectorToSnap.z - minZ) ? maxZ : minZ);
        return vectorToSnap;
    }

}
