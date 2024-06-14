using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;


public class Players
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public string Region { get; set; }
}

public class ASM_MN
{
    private static List<Players> listPlayer = new List<Players>();
    internal static object Instance;

    // Y01: Hàm YC1 để lưu thông tin người chơi
    public static void YC1(int id, string name, int score, string region)
    {
        Players player = new Players
        {
            Id = id,
            Name = name,
            Score = score,
            Region = region
        };

        listPlayer.Add(player);
    }

    // Y02: Hàm calculate_rank để tính rank
    public static string calculate_rank(int score)
    {
        if (score < 100)
            return "Hạng đồng";
        else if (score < 500)
            return "Bạc";
        else if (score < 1000)
            return "Vàng";
        else
            return "Kim cương";
    }

    // Hàm YC2 để xuất danh sách người chơi
    public static void YC2()
    {
        foreach (var player in listPlayer)
        {
            string rank = calculate_rank(player.Score);
            Console.WriteLine($"Id: {player.Id}, Name: {player.Name}, Score: {player.Score}, Region: {player.Region}, Rank: {rank}");

        }
    }

    // Y03: Hàm YC3 để xuất thông tin các player có score bé hơn score hiện tại
    public static void YC3(int currentScore)
    {
        var filteredPlayers = listPlayer.Where(p => p.Score < currentScore).ToList();
        foreach (var player in filteredPlayers)
        {
            Console.WriteLine($"Id: {player.Id}, Name: {player.Name}, Score: {player.Score}, Region: {player.Region}");
        }
    }

    // Y04: Hàm YC4 sử dụng Linq để tìm Player theo Id
    public static void YC4(int id)
    {
        var player = listPlayer.FirstOrDefault(p => p.Id == id);
        if (player != null)
        {
            Console.WriteLine($"Id: {player.Id}, Name: {player.Name}, Score: {player.Score}, Region: {player.Region}");
        }
    }

    // Y05: Hàm YC5 xuất thông tin các Player theo thứ tự score giảm dần
    public static void YC5()
    {
        var sortedPlayers = listPlayer.OrderByDescending(p => p.Score).ToList();
        foreach (var player in sortedPlayers)
        {
            Console.WriteLine($"Id: {player.Id}, Name: {player.Name}, Score: {player.Score}, Region: {player.Region}");
        }
    }

    // Y06: Hàm YC6 xuất thông tin 5 player có score thấp nhất theo thứ tự tăng dần
    public static void YC6()
    {
        var lowestPlayers = listPlayer.OrderBy(p => p.Score).Take(5).ToList();
        foreach (var player in lowestPlayers)
        {
            Console.WriteLine($"Id: {player.Id}, Name: {player.Name}, Score: {player.Score}, Region: {player.Region}");
        }
    }

    // Y07: Hàm YC7 tạo Thread tính score trung bình dựa trên Region và lưu vào tập tin bxhReig
    public static void YC7()
    {
        Thread thread = new Thread(new ThreadStart(CalculateAverageScoreByRegion));
        thread.Name = "BXH";
        thread.Start();
    }

    private static void CalculateAverageScoreByRegion()
    {
        var regionGroups = listPlayer.GroupBy(p => p.Region).ToList();
        var regionAverages = new Dictionary<string, double>();

        foreach (var group in regionGroups)
        {
            double averageScore = group.Average(p => p.Score);
            regionAverages[group.Key] = averageScore;
        }

        using (StreamWriter writer = new StreamWriter("bxhReig.txt"))
        {
            foreach (var entry in regionAverages)
            {
                writer.WriteLine($"Region: {entry.Key}, Average Score: {entry.Value}");
            }
        }
    }
}
