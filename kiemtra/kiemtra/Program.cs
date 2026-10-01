using System;
using System.Collections.Generic;
using System.Linq;

abstract class PhuongTien
{
    private string _maPT;
    private string _tenHang;
    private int _namSanXuat;
    private decimal _giaGoc;

    public string MaPT
    {
        get { return _maPT; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                _maPT = "PT000";
            else
                _maPT = value;
        }
    }

    public string TenHang
    {
        get { return _tenHang; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tên hãng không được để trống!");

            _tenHang = value;
        }
    }

    public int NamSanXuat
    {
        get { return _namSanXuat; }
        set
        {
            int namHienTai = DateTime.Now.Year;

            if (value < 1900 || value > namHienTai)
                throw new ArgumentException("Năm sản xuất không hợp lệ!");

            _namSanXuat = value;
        }
    }

    public decimal GiaGoc
    {
        get { return _giaGoc; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Giá gốc phải lớn hơn 0!");

            _giaGoc = value;
        }
    }

    public PhuongTien(
        string maPT,
        string tenHang,
        int namSanXuat,
        decimal giaGoc)
    {
        MaPT = maPT;
        TenHang = tenHang;
        NamSanXuat = namSanXuat;
        GiaGoc = giaGoc;
    }

    public abstract decimal TinhGiaLanBanh();

    public virtual string GetInfo()
    {
        return $"Mã: {MaPT}, Hãng: {TenHang}, " +
               $"Năm SX: {NamSanXuat}, " +
               $"Giá gốc: {GiaGoc:N0} VNĐ";
    }
}

class OTo : PhuongTien
{
    private int _soChoNgoi;
    private double _dungTichDongCo;

    public int SoChoNgoi
    {
        get { return _soChoNgoi; }
        set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Số chỗ ngồi phải lớn hơn 0!");

            _soChoNgoi = value;
        }
    }

    public double DungTichDongCo
    {
        get { return _dungTichDongCo; }
        set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Dung tích động cơ phải lớn hơn 0!");

            _dungTichDongCo = value;
        }
    }
    public OTo(
        string maPT,
        string tenHang,
        int namSanXuat,
        decimal giaGoc,
        int soChoNgoi,
        double dungTichDongCo)
        : base(maPT, tenHang, namSanXuat, giaGoc)
    {
        SoChoNgoi = soChoNgoi;
        DungTichDongCo = dungTichDongCo;
    }

    public override decimal TinhGiaLanBanh()
    {
        if (SoChoNgoi <= 9)
        {
            return GiaGoc
                + GiaGoc * 0.12m
                + GiaGoc * 0.30m;
        }
        else
        {
            return GiaGoc
                + GiaGoc * 0.10m;
        }
    }

    public override string GetInfo()
    {
        return base.GetInfo()
            + $", Số chỗ: {SoChoNgoi}"
            + $", Động cơ: {DungTichDongCo}L";
    }
}
class XeMay : PhuongTien
{
    private int _dungTichXylanh;

    public int DungTichXylanh
    {
        get { return _dungTichXylanh; }
        set
        {
            if (value <= 0)
                throw new ArgumentException(
                    "Dung tích xy-lanh phải lớn hơn 0!");

            _dungTichXylanh = value;
        }
    }

    public XeMay(
        string maPT,
        string tenHang,
        int namSanXuat,
        decimal giaGoc,
        int dungTichXylanh)
        : base(maPT, tenHang, namSanXuat, giaGoc)
    {
        DungTichXylanh = dungTichXylanh;
    }

    public override decimal TinhGiaLanBanh()
    {
        if (DungTichXylanh < 175)
        {
            return GiaGoc + GiaGoc * 0.02m;
        }
        else
        {
            return GiaGoc + GiaGoc * 0.05m;
        }
    }

    public override string GetInfo()
    {
        return base.GetInfo()
            + $", Xy-lanh: {DungTichXylanh}cc";
    }
}

class QuanLyPhuongTien
{
    private List<PhuongTien> danhSach = new List<PhuongTien>();

    public void AddPhuongTien(PhuongTien pt)
    {
        danhSach.Add(pt);
    }

    public void DisplayAll()
    {
        Console.WriteLine("\n===== DANH SACH PHUONG TIEN =====");

        foreach (PhuongTien pt in danhSach)
        {
            Console.WriteLine(pt.GetInfo());
            Console.WriteLine(
                $"Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ");
            Console.WriteLine("--------------------------------");
        }
    }

    public PhuongTien FindMaxGiaLanBanh()
    {
        return danhSach
            .OrderByDescending(pt => pt.TinhGiaLanBanh())
            .FirstOrDefault();
    }

    public List<PhuongTien> SearchByName(string keyword)
    {
        return danhSach
            .Where(pt => pt.TenHang
            .Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("\n===== TC01 =====");
        Console.WriteLine("Kiểm tra năm sản xuất = 1850");

        // TC01 - VALIDATION NAM SAN XUAT
        try
        {
            Console.Write("Nhập mã phương tiện: ");
            string ma = Console.ReadLine();

            Console.Write("Nhập tên hãng: ");
            string hang = Console.ReadLine();

            Console.Write("Nhập năm sản xuất: ");
            int nam = int.Parse(Console.ReadLine());

            Console.Write("Nhập giá gốc: ");
            decimal gia = decimal.Parse(Console.ReadLine());

            Console.Write("Nhập số chỗ: ");
            int cho = int.Parse(Console.ReadLine());

            Console.Write("Nhập dung tích động cơ: ");
            double dongCo = double.Parse(Console.ReadLine());

            OTo oto = new OTo(
                ma, hang, nam, gia, cho, dongCo
            );

            Console.WriteLine("→ TC01: Tạo phương tiện thành công!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("→ TC01: BẮT ĐƯỢC NGOẠI LỆ");
            Console.WriteLine($"→ {ex.Message}");
        }

        // TC02 - TINH GIA OTO
        Console.WriteLine("\n===== TC02 =====");

        try
        {
            Console.Write("Nhập mã: ");
            string ma = Console.ReadLine();

            Console.Write("Nhập hãng: ");
            string hang = Console.ReadLine();

            Console.Write("Nhập năm sản xuất: ");
            int nam = int.Parse(Console.ReadLine());

            Console.Write("Nhập giá gốc: ");
            decimal gia = decimal.Parse(Console.ReadLine());

            Console.Write("Nhập số chỗ: ");
            int cho = int.Parse(Console.ReadLine());

            Console.Write("Nhập dung tích động cơ (L): ");
            double dongCo = double.Parse(Console.ReadLine());

            OTo oto = new OTo(
                ma, hang, nam, gia, cho, dongCo
            );

            Console.WriteLine("\nThông tin ô tô:");
            Console.WriteLine(oto.GetInfo());

            decimal giaLanBanh =
                oto.TinhGiaLanBanh();

            Console.WriteLine(
                $"Giá lăn bánh: {giaLanBanh:N0} VNĐ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"→ Lỗi: {ex.Message}");
        }

        // TC03 - TINH GIA XE MAY
        Console.WriteLine("\n===== TC03 =====");

        try
        {
            Console.Write("Nhập mã: ");
            string ma = Console.ReadLine();

            Console.Write("Nhập hãng: ");
            string hang = Console.ReadLine();

            Console.Write("Nhập năm sản xuất: ");
            int nam = int.Parse(Console.ReadLine());

            Console.Write("Nhập giá gốc: ");
            decimal gia = decimal.Parse(Console.ReadLine());

            Console.Write("Nhập dung tích xy-lanh (cc): ");
            int cc = int.Parse(Console.ReadLine());

            XeMay xeMay = new XeMay(
                ma, hang, nam, gia, cc
            );

            Console.WriteLine("\nThông tin xe máy:");
            Console.WriteLine(xeMay.GetInfo());

            decimal giaLanBanh =
                xeMay.TinhGiaLanBanh();

            Console.WriteLine(
                $"Giá lăn bánh: {giaLanBanh:N0} VNĐ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"→ Lỗi: {ex.Message}");
        }

        // TC04 - DA HINH
        Console.WriteLine("\n===== TC04 =====");

        QuanLyPhuongTien ql =
        new QuanLyPhuongTien();

        try
        {
            Console.WriteLine("\nNhập Ô tô ");

            Console.Write("Mã: ");
            string maOto = Console.ReadLine();

            Console.Write("Hãng: ");
            string hangOto = Console.ReadLine();

            Console.Write("Năm sản xuất: ");
            int namOto = int.Parse(Console.ReadLine());

            Console.Write("Giá gốc: ");
            decimal giaOto = decimal.Parse(Console.ReadLine());

            Console.Write("Số chỗ: ");
            int choOto = int.Parse(Console.ReadLine());

            Console.Write("Dung tích động cơ: ");
            double dongCo = double.Parse(Console.ReadLine());

            OTo oto = new OTo(
                maOto,
                hangOto,
                namOto,
                giaOto,
                choOto,
                dongCo
            );

            Console.WriteLine("\nNhập Xe máy ");

            Console.Write("Mã: ");
            string maXe = Console.ReadLine();

            Console.Write("Hãng: ");
            string hangXe = Console.ReadLine();

            Console.Write("Năm sản xuất: ");
            int namXe = int.Parse(Console.ReadLine());

            Console.Write("Giá gốc: ");
            decimal giaXe = decimal.Parse(Console.ReadLine());

            Console.Write("Dung tích xy-lanh: ");
            int cc = int.Parse(Console.ReadLine());

            XeMay xeMay = new XeMay(
                maXe,
                hangXe,
                namXe,
                giaXe,
                cc
            );

            ql.AddPhuongTien(oto);
            ql.AddPhuongTien(xeMay);

            Console.WriteLine("\n--- KẾT QUẢ ĐA HÌNH ---");

            ql.DisplayAll();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"→ Lỗi: {ex.Message}");
        }

        // TC05 - TIM GIA MAX
        try
        {
            Console.Write("Nhập số lượng phương tiện: ");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(
                    $"\n--- Phương tiện thứ {i + 1} ---");

                Console.Write("Loại (1 = Ô tô, 2 = Xe máy): ");
                int loai = int.Parse(Console.ReadLine());

                Console.Write("Mã: ");
                string ma = Console.ReadLine();

                Console.Write("Hãng: ");
                string hang = Console.ReadLine();

                Console.Write("Năm sản xuất: ");
                int nam = int.Parse(Console.ReadLine());

                Console.Write("Giá gốc: ");
                decimal gia = decimal.Parse(Console.ReadLine());

                if (loai == 1)
                {
                    Console.Write("Số chỗ: ");
                    int cho = int.Parse(Console.ReadLine());

                    Console.Write("Dung tích động cơ: ");
                    double dongCo = double.Parse(Console.ReadLine());

                    OTo oto = new OTo(
                        ma,
                        hang,
                        nam,
                        gia,
                        cho,
                        dongCo
                    );

                    ql.AddPhuongTien(oto);
                }
                else
                {
                    Console.Write("Dung tích xy-lanh: ");
                    int cc = int.Parse(Console.ReadLine());

                    XeMay xeMay = new XeMay(
                        ma,
                        hang,
                        nam,
                        gia,
                        cc
                    );

                    ql.AddPhuongTien(xeMay);
                }
            }

            PhuongTien max =
                ql.FindMaxGiaLanBanh();

            Console.WriteLine("\n--- KẾT QUẢ TC05 ---");

            Console.WriteLine(max.GetInfo());

            Console.WriteLine(
                $"Giá lăn bánh cao nhất: " +
                $"{max.TinhGiaLanBanh():N0} VNĐ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"→ Lỗi: {ex.Message}");
        }

        Console.ReadKey();
    }
}
