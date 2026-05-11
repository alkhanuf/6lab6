using System;
using System.Collections.Generic;
using System.Text;

namespace _6lab6
{
    public abstract class IImpactPoint
    {
        public float X;
        public float Y;

        public abstract void ImpactParticle(Particle particle);

        public virtual void Render(Graphics g)
        {
            
        }
    }

    public class GravityPoint : IImpactPoint
    {
        public int Power = 100;

        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            double r = Math.Sqrt(gX * gX + gY * gY);
            //if (r + particle.Radius < Power / 2)
            //{
                float r2 = (float)Math.Max(100, gX * gX + gY * gY);
                particle.SpeedX += gX * Power / r2;
                particle.SpeedY += gY * Power / r2;
            //}

        }

        public override void Render(Graphics g)
        {
            
            g.DrawEllipse(
                   new Pen(Color.Red),
                   X - Power / 2,
                   Y - Power / 2,
                   Power,
                   Power
            );

            //var stringFormat = new StringFormat();
            //stringFormat.Alignment = StringAlignment.Center; 
            //stringFormat.LineAlignment = StringAlignment.Center; 

            //g.DrawString(
            //    $"Я гравитон\nc силой {Power}",
            //    new Font("Verdana", 10),
            //    new SolidBrush(Color.DarkRed),
            //    X,
            //    Y,
            //    stringFormat 
            //);
        }

    }

    public class AntiGravityPoint : IImpactPoint
    {
        public int Power = 100;

        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            float r2 = (float)Math.Max(100, gX * gX + gY * gY);

            particle.SpeedX -= gX * Power / r2;
            particle.SpeedY -= gY * Power / r2;
        }
    }

    public class ColorPoint : IImpactPoint
    {
        public Color OutColor = Color.Purple;
        public int Radius = 60;

        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            double r = Math.Sqrt(gX * gX + gY * gY);

            if (r + particle.Radius < Radius && particle is ParticleColorful colorParticle)
            {
                colorParticle.FromColor = OutColor;
                colorParticle.ToColor = Color.FromArgb(0, Color.Black);
            }
        }

        public override void Render(Graphics g)
        {

            g.DrawEllipse(
                new Pen(OutColor),
                X - Radius,
                Y - Radius,
                Radius * 2,
                Radius * 2
            );
        }

    }

    public class CounterPoint : IImpactPoint
    {
        public int Radius = 40;
        public int Count = 0;

        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            double r = Math.Sqrt(gX * gX + gY * gY);

            if (r + particle.Radius < Radius)
            {
                particle.Life = 0;
                Count++;
            }
        }

        public override void Render(Graphics g)
        {


            g.DrawEllipse(
                new Pen(Color.Orange),
                X - Radius,
                Y - Radius,
                Radius * 2,
                Radius * 2
                );

            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            g.DrawString(
                $"Счет: {Count}",
                new Font("Verdana", 10),
                new SolidBrush(Color.Black),
                X,
                Y,
                stringFormat
            );
        }
    }

}