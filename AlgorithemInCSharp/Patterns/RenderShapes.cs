namespace AlgorithemInCSharp.Patterns
{
    public interface IRenderer
    {
        void RenderCirecle(string radius);
        void RenderSquare(string side);
        void RenderTriangle(string side);
    }

    public class VectorRender : IRenderer
    {
        public void RenderCirecle(string radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius} using vector graphics.");
        }

        public void RenderSquare(string side)
        {
            Console.WriteLine($"Drawing a square with side {side} using vector graphics.");
        }

        public void RenderTriangle(string side)
        {
            Console.WriteLine($"Drawing an equilateral triangle with side {side} using vector graphics.");
        }
    }

    public class RasterRender : IRenderer
    {
        public void RenderCirecle(string radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius} using pixels.");
        }

        public void RenderSquare(string side)
        {
            Console.WriteLine($"Drawing a square with side {side} using pixels.");
        }

        public void RenderTriangle(string side)
        {
              Console.WriteLine($"Drawing an equilateral triangle with side {side} using pixels.");
        }
    }
}