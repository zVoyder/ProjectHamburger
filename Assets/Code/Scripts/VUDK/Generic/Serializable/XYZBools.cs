namespace VUDK.Generic.Serializable
{
    [System.Serializable]
    public struct XYZBools
    {
        public bool X, Y, Z;

        public XYZBools(bool x, bool y, bool z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}