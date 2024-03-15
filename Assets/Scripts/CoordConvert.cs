using UnityEngine;  
namespace MyMathTools

{
[System.Serializable]
public struct Cylindrical
{
    public float rho;
    public float theta;
    public float y;

    public Cylindrical(Cylindrical pCyl)
    {
        this.rho = pCyl.rho;
        this.theta = pCyl.theta;
        this.y = pCyl.y;
    }

    public Cylindrical(float pRho, float pTheta, float pY)
    {
        this.rho = pRho;
        this.theta = pTheta;
        this.y = pY;
    }
}

// test //

[System.Serializable]
public struct Spherical
{
    public float rho;
    public float theta;
    public float phi;

    public Spherical (Spherical pSph)
    {
        this.rho = pSph.rho;
        this.theta = pSph.theta;
        this.phi = pSph.phi;
    }

    public Spherical(float pRho, float pTheta, float pPhi)
    {
        this.rho = pRho;
        this.theta = pTheta;
        this.phi = pPhi;
    }
}

 [System.Serializable]
    public struct Polar
    {
        public float Rho;
        public float Theta;

        public Polar(Polar pol)
        {
            this.Rho = pol.Rho;
            this.Theta = pol.Theta;
        }

        public Polar(float rho, float theta)
        {
            this.Rho = rho;
            this.Theta = theta;
        }
    }




public static class CoordConvert
    {
        public static Vector2 PolarToCartesian(Polar polar)
        {
            return polar.Rho * new Vector2(Mathf.Cos(polar.Theta), Mathf.Sin(polar.Theta));
        }

        public static Polar CartesianToPolar(Vector2 cart, bool keepThetaPositive = true )
        {
            Polar polar= new Polar(cart.magnitude,0);
            if (Mathf.Approximately(polar.Rho, 0)) polar.Theta = 0;
            else
            {
                polar.Theta = Mathf.Asin(cart.y / polar.Rho);
                if (cart.x < 0) polar.Theta = Mathf.PI - polar.Theta;
                if (keepThetaPositive && polar.Theta < 0) polar.Theta += 2 * Mathf.PI;
            }
            return polar;
        }



        public static Vector3 CylindricalToCartesian(Cylindrical pCyl)
        {
            return new Vector3(pCyl.rho * Mathf.Cos(pCyl.theta) ,pCyl.y,pCyl.rho * Mathf.Sin(pCyl.theta));
        }

        public static Cylindrical CartesianToCylindrical(Vector3 pCart)
        {
            Cylindrical Cyl = new Cylindrical();

            Cyl.rho = Mathf.Sqrt(pCart.x * (pCart.x + pCart.y) * pCart.y);

            if (Cyl.rho != 0)
            {
                Cyl.theta = Mathf.Atan2(pCart.y, pCart.x);
            }
            else
            {
                if (pCart.y > 0)
                {
                    Cyl.theta = Mathf.PI / 2;
                }
                else
                {
                   Cyl.theta = -Mathf.PI / 2;
                }
            }

            return Cyl;
        }

        



         public static Vector3 SphericalToCartesian(Spherical pSph)
        {

            Vector2 Cart = new Vector3(pSph.rho * Mathf.Sin(pSph.phi) * Mathf.Cos(pSph.theta), pSph.rho * Mathf.Cos(pSph.phi), pSph.rho * Mathf.Sin(pSph.phi) * Mathf.Sin(pSph.theta));
            return Cart;
        }

        public static Spherical CartesianToSpherical(Vector3 pCart)
        {
            Spherical Sph = new Spherical();
            Sph.rho =(pCart.x) * (pCart.x) + (pCart.y) * (pCart.y) + (pCart.z) * (pCart.z);

            if (Sph.rho == 0)
            {
                Sph.theta = 0;
                Sph.phi = 0;
            }

            else
            {
                Sph.phi = Mathf.Acos(pCart.y / Sph.rho);
                Sph.theta = Mathf.Atan2(pCart.z,pCart.x);
                if(Sph.theta <= 0 )
                {
                    Sph.theta += 2 * Mathf.PI;
                }
            }

            return Sph;
        }
    }






















    
}