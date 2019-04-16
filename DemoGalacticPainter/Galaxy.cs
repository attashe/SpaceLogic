using System;
using System.Collections.Generic;
namespace DemoGalacticPainter
{
    public class Galaxy
    {
        private const int MAX_ITER = 10;
        Random random = new Random();
        public List<Star> stars = new List<Star>();
        public List<Hyperlane> hyperlanes = new List<Hyperlane>();
        public List<Star> GenerateStars(int N, int M, int K, double a, double minDist) {
            stars = new List<Star>();
            int[,] galacticMap = new int[N, M];
            double diametr = (Math.Sqrt(2) * a - minDist);
            for (int i = 0; i < K; i++) {
                int iter = 0;
                while (iter < MAX_ITER) {
                    iter++;
                    int y = random.Next(0, N);
                    int x = random.Next(0, M);

                    if (galacticMap[y, x] == 0) {
                        galacticMap[y, x] = 1;
                        double shiftX = (random.NextDouble() - 0.5) * diametr;
                        double shiftY = (random.NextDouble() - 0.5) * diametr;

                        stars.Add(new Star(y * a + shiftY, x * a + shiftX));
                        break;
                    } 
                }
            }
            return stars;
        }

        Dictionary<int, Star> d = new Dictionary<int, Star>();
        private void addHyp(Star s1, Star s2) {
            int k = (int)(s1.x * 1000 + s1.y);
            if (d.ContainsKey(k))
            {
                //s1.x = -10;
                //s1.y = -10;
                s1 = d[k];
            }
            else
            {
                stars.Add(s1);
                d.Add(k, s1);
            }
            k = (int)(s2.x * 1000 + s2.y);
            if (d.ContainsKey(k))
            {
                //s2.x = -10;
                //s2.y = -10;
                s2 = d[k];
            }
            else
            {
                stars.Add(s2);
                d.Add(k, s2);
            }
            hyperlanes.Add(new Hyperlane(s1, s2));
        }

        private void addTemplate(double prob,Star s1, Star s2, Star s3, 
                                 Star s4, Star s5, Star s6) {
            if (random.NextDouble() < prob) {
                addHyp(s1, s2);
            }
            if (random.NextDouble() < prob) {
                addHyp(s3, s4);
            }
            if (random.NextDouble() < prob) {
                addHyp(s5, s6);
            }
        }

        private void templates(List<Star> tempStars) {
            double prob = 0.3;
            int l = tempStars.Count;
            double r = random.NextDouble();
            Console.WriteLine(r);
            if (r < 1.0 / 8) {
                addTemplate(prob, tempStars[l-1], tempStars[l-5],
                            tempStars[l-5], tempStars[l-3],
                            tempStars[l-3], tempStars[l-1]);
            } else
            if (r < 2.0 / 8)
            {
                addTemplate(prob, tempStars[l-1], tempStars[l-5],
                            tempStars[l-5], tempStars[l-2],
                            tempStars[l-2], tempStars[l-4]);
            } else
            if (r < 3.0 / 8)
            {
                addTemplate(prob, tempStars[l-1], tempStars[l-5],
                            tempStars[l-4], tempStars[l-1],
                            tempStars[l-2], tempStars[l-4]);
            } else
            if (r < 4.0 / 8)
            {
                addTemplate(prob, tempStars[l-6], tempStars[l-3],
                            tempStars[l-3], tempStars[l-1],
                            tempStars[l-3], tempStars[l-5]);
            } else
            if (r < 5.0 / 8)
            {
                addTemplate(prob, tempStars[l-2], tempStars[l-4],
                            tempStars[l-2], tempStars[l-6],
                            tempStars[l-6], tempStars[l-4]);
            } else
            if (r < 6.0 / 8)
            {
                addTemplate(prob, tempStars[l-2], tempStars[l-6],
                            tempStars[l-6], tempStars[l-4],
                            tempStars[l-3], tempStars[l-6]);
            } else
            if (r < 7.0 / 8)
            {
                addTemplate(prob, tempStars[l-6], tempStars[l-3],
                            tempStars[l-1], tempStars[l-3],
                            tempStars[l-6], tempStars[l-4]);
            } else
            {
                addTemplate(prob, tempStars[l-6], tempStars[l-3],
                            tempStars[l-5], tempStars[l-3],
                            tempStars[l-2], tempStars[l-6]);
            }
            return;
        }

        public List<Star> GenerateHexStars(int N, int M, double R) {
            List<(double, double)> centers = new List<(double, double)>();
            double r = R * Math.Sqrt(3) / 2;
            for (int i = 0; i < N; i++) {
                for (int j = 0; j < M; j++) {
                    centers.Add((i * 2 * r + r * (j % 2), j * 1.5 * R));
                }
            }
            List<Star> tempStars = new List<Star>();
            foreach (var p in centers) {
                tempStars.Add(new Star(p.Item1, p.Item2 + R));
                tempStars.Add(new Star(p.Item1 + r, p.Item2 + 0.5 * R));
                tempStars.Add(new Star(p.Item1 + r, p.Item2 - 0.5 * R));
                tempStars.Add(new Star(p.Item1, p.Item2 - R));
                tempStars.Add(new Star(p.Item1 - r, p.Item2 - 0.5 * R));
                tempStars.Add(new Star(p.Item1 - r, p.Item2 + 0.5 * R));

                int l = tempStars.Count;
                addHyp(tempStars[l-2], tempStars[l-1]);
                addHyp(tempStars[l-3], tempStars[l-2]);
                addHyp(tempStars[l-4], tempStars[l-3] );
                addHyp(tempStars[l-5], tempStars[l-5]);
                addHyp(tempStars[l-5], tempStars[l-6]);
                addHyp(tempStars[l-6], tempStars[l-1]);

                if (random.NextDouble() < 0.7f) {
                    templates(tempStars);
                }
                //if (random.NextDouble() < 0.33) {
                //    addHyp(tempStars[l - 2], tempStars[l - 1]);
                //    addHyp(tempStars[l - 3], tempStars[l - 1]);
                //    addHyp(tempStars[l - 2], tempStars[l - 1]);
                //} else {
                //    if (random.NextDouble() < 0.5) {
                //        addHyp(tempStars[l - 2], tempStars[l - 1]);
                //        addHyp(tempStars[l - 2], tempStars[l - 1]);
                //        addHyp(tempStars[l - 2], tempStars[l - 1]);
                //    }
                //    else {
                //        addHyp(tempStars[l - 2], tempStars[l - 1]);
                //        addHyp(tempStars[l - 2], tempStars[l - 1]);
                //        addHyp(tempStars[l - 2], tempStars[l - 1]);
                //    }
                //}
            }
           
            //for (int i = 0; i < tempStars.Count; i++) {
            //    int k = (int)(tempStars[i].x * 1000 + tempStars[i].y);
            //    if (d.ContainsKey(k)) {
            //        System.Console.WriteLine("Deleted v " + k);
            //        tempStars[i].x = -10f;
            //        tempStars[i].y = -10f;
            //        //tempStars.RemoveAt(i);
            //        //i--;
            //    }
            //    else {
            //        d.Add(k, tempStars[i]);
            //    }
            //}
            for (int i = 0; i < tempStars.Count; i++) {
                double EPSILON = 1e-4;
                if ((Math.Abs(tempStars[i].x - -10) < EPSILON) ||
                    (Math.Abs(tempStars[i].y - -10) < EPSILON)) {
                } else {
                    tempStars[i].x += (random.NextDouble() - 0.5) * r * 0.9;
                    tempStars[i].y += (random.NextDouble() - 0.5) * r * 0.9;
                }
            }
               
            int len = hyperlanes.Count;
            for (int i = 0; i < len * 0.2f; i++) {
                hyperlanes.RemoveAt(random.Next(hyperlanes.Count));
            }

            return tempStars;
        }

        public List<Hyperlane> GenerateHyperlanes(List<Star> stars, double prob) {
            for (int i = 0; i < stars.Count; i++) {
                for (int j = 0; j < stars.Count; j++) {
                    if (i != j && random.NextDouble() < prob) {
                        hyperlanes.Add(new Hyperlane(stars[i], stars[j]));
                    }
                }
            }
            return hyperlanes;
        }
        public Galaxy()
        {
        }
    }
}
