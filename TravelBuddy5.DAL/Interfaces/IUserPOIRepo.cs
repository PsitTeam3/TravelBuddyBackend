namespace TravelBuddy5.DAL.Interfaces
{
    public interface IUserPOIRepo
    {
        void SetPOIAsVisited(int poiID, int tourID);

        POI GetNextPOI(int userTourId);
        POI GetNextPOI(UserTour usertour);
    }
}
