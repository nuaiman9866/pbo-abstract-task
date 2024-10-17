interface Kemampuan
{
    void gunakan(Robot robot);
    bool cooldownSelesai();
}

abstract class Robot
{
    public string nama;
    public int energi;
    public int armor;
    public int serangan;

    public Robot(string nama, int energi, int armor, int serangan)
    {
        this.nama = nama;
        this.energi = energi;
        this.armor = armor;
        this.serangan = serangan;
    }

    public void serang(Robot target)
    {
        int damage = serangan - target.armor;
        if (damage > 0)
        {
            target.energi -= damage;
            Console.WriteLine(nama + " menyerang " + target.nama + " dengan damage " + damage + "!");
        }
        else
        {
            Console.WriteLine(nama + " menyerang " + target.nama + " tetapi tidak ada damage!");
        }
    }

    public void cetakInformasi()
    {
        Console.WriteLine("Nama: " + nama);
        Console.WriteLine("Energi: " + energi);
        Console.WriteLine("Armor: " + armor);
        Console.WriteLine("Serangan: " + serangan);
    }

    public abstract void gunakanKemampuan(Kemampuan kemampuan);

    public bool masihHidup()
    {
        return energi > 0;
    }
}

class RobotBiasa : Robot
{

    public RobotBiasa(string nama, int energi, int armor, int serangan) : base(nama, energi, armor, serangan)
    {
    }

    public override void gunakanKemampuan(Kemampuan kemampuan)
    {
        if (kemampuan.cooldownSelesai())
        {
            kemampuan.gunakan(this);
        }
        else
        {
            Console.WriteLine("Kemampuan masih dalam cooldown!");
        }
    }
}

class BosRobot : Robot
{
    private int pertahanan;

    public BosRobot(string nama, int energi, int armor, int serangan, int pertahanan) : base(nama, energi, armor, serangan)
    {
        this.pertahanan = pertahanan;
    }

    public override void gunakanKemampuan(Kemampuan kemampuan)
    {
        if (kemampuan.cooldownSelesai())
        {
            kemampuan.gunakan(this);
        }
        else
        {
            Console.WriteLine("Kemampuan masih dalam cooldown!");
        }
    }

    public void diserang(Robot penyerang)
    {
        int damage = penyerang.serangan - (armor + pertahanan);
        if (damage > 0)
        {
            energi -= damage;
            Console.WriteLine(nama + " diserang oleh " + penyerang.nama + " dengan damage " + damage + "!");
        }
        else
        {
            Console.WriteLine(nama + " berhasil menahan serangan dari " + penyerang.nama + "!");
        }

        if (energi <= 0)
        {
            mati();
        }
    }

    public void mati()
    {
        Console.WriteLine(nama + " telah mati!");
    }
}

class Perbaikan : Kemampuan
{
    private int pemulihan;
    private int cooldown;
    private int cooldownTimer;

    public Perbaikan(int pemulihan, int cooldown)
    {
        this.pemulihan = pemulihan;
        this.cooldown = cooldown;
        this.cooldownTimer = 0;
    }

    public void gunakan(Robot robot)
    {
        robot.energi += pemulihan;
        Console.WriteLine(robot.nama + " menggunakan kemampuan Perbaikan dan memulihkan " + pemulihan + " energi!");
        cooldownTimer = cooldown;
    }

    public bool cooldownSelesai()
    {
        return cooldownTimer == 0;
    }

    public void kurangiCooldown()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer--;
        }
    }
}

class SeranganListrik : Kemampuan
{
    private int damage;
    private int cooldown;
    private int cooldownTimer;

    public SeranganListrik(int damage, int cooldown)
    {
        this.damage = damage;
        this.cooldown = cooldown;
        this.cooldownTimer = 0;
    }

    public void gunakan(Robot robot)
    {
        Console.WriteLine(robot.nama + " menggunakan kemampuan Serangan Listrik dan memberikan damage " + damage + "!");
        cooldownTimer = cooldown;
    }

    public bool cooldownSelesai()
    {
        return cooldownTimer == 0;
    }

    public void kurangiCooldown()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer--;
        }
    }
}

class SeranganPlasma : Kemampuan
{
    private int damage;
    private int cooldown;
    private int cooldownTimer;

    public SeranganPlasma(int damage, int cooldown)
    {
        this.damage = damage;
        this.cooldown = cooldown;
        this.cooldownTimer = 0;
    }

    public void gunakan(Robot robot)
    {
        Console.WriteLine(robot.nama + " menggunakan kemampuan Serangan Plasma dan memberikan damage " + damage + "!");
        cooldownTimer = cooldown;
    }

    public bool cooldownSelesai()
    {
        return cooldownTimer == 0;
    }

    public void kurangiCooldown()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer--;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        RobotBiasa robot12 = new RobotBiasa("Black Faiz", 100, 10, 20);
        RobotBiasa robot23 = new RobotBiasa("Ironi Man", 80, 15, 15);

        BosRobot bos44Robot = new BosRobot("Ambatron", 200, 20, 30, 10);

        Kemampuan perbaikan = new Perbaikan(30, 3);
        Kemampuan listrik = new SeranganListrik(25, 2);
        Kemampuan plasma = new SeranganPlasma(40, 4);

        robot12.cetakInformasi();
        robot23.cetakInformasi();
        bos44Robot.cetakInformasi();

        robot12.serang(bos44Robot);
        bos44Robot.diserang(robot12);

        robot23.gunakanKemampuan(plasma);
        robot23.gunakanKemampuan(listrik);
    }
}
