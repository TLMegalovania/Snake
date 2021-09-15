using System;
using Xunit;
using Snake;
using Xunit.Abstractions;

namespace SnakeTest
{
    public class SGenTest
    {
        SnakeGenerator generator;
        ITestOutputHelper console;
        int r, c;
        public SGenTest(ITestOutputHelper helper)
        {
            var rand = new Random();
            r = rand.Next(1,int.MaxValue);
            c = rand.Next(1,int.MaxValue);
            generator = new SnakeGenerator(r,c);
            console = helper;
            console.WriteLine($"Row : {r}, Column : {c}");
        }
        [Fact]
        public void GenSnakeTest()
        {
            for(int i = 0; i < 100; i++)
            {
                GenTest();
            }
        }
        void GenTest()
        {
            var snake = generator.GenSnake();
            Assert.All(snake, point =>
            {
                Assert.True(point[0] >= 0 && point[0] < r && point[1] >= 0 && point[1] < c,
                    $"Got ({point[0]},{point[1]})");
            });
        }
    }
}
