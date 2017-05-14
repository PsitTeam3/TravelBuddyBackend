namespace TravelBuddy5.DAL.Interfaces
{
    public interface IUserPOIRepo
    {
        /// <summary>
        /// Sets the POI as visited.
        /// </summary>
        /// <param name="poiID">The poi identifier.</param>
        /// <param name="tourID">The tour identifier.</param>
        void SetPOIAsVisited(int poiID, int tourID);

        /// <summary>
        /// Gets the next POI for a given user.
        /// </summary>
        /// <param name="userTourId">The user tour identifier.</param>
        /// <returns>Data object of the next POI in the active tour from the given user</returns>
        POI GetNextPOI(int userTourId);

        /// <summary>
        /// Gets the next POI for a given usertour (started tour-instance from a specific user).
        /// </summary>
        /// <param name="usertour">The usertour.</param>
        /// <returns>ata object of the next POI in the given tour instance</returns>
        POI GetNextPOI(UserTour usertour);
    }
}
