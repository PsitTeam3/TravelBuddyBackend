namespace TravelBuddy5.DAL.Interfaces
{
    public interface IUserPOIRepo
    {
        void SetPOIAsVisited(int poiID, int tourID);
        void SetPOIAsVisited(int poiID, int tourID, int userID);

        POI GetNextPOI(int userTourId);
        POI GetNextPOI(UserTour usertour);
    }
}
