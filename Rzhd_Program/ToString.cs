namespace Rzhd_Program
{
    public partial class VidWagon
    {
        public override string ToString()
        {
            return vid_VidWagon;
        }
    }
    public partial class VidRepair
    {
        public override string ToString()
        {
            return vid_VidRepair;
        }
    }
    public partial class Wagons
    {
        public override string ToString()
        {
            return "Вагон: " + code_Wagon + " " + VidWagon.vid_VidWagon;
        }
    }
    public partial class Repair
    {
        public override string ToString()
        {
            return "Вагон: " + Wagons.code_Wagon + " " + Wagons.VidWagon.vid_VidWagon + "\nРемонт: " + VidRepair.vid_VidRepair + "\n" + status_Repair;
        }
    }
}
