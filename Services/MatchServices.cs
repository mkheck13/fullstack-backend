using fullstack_backend.Context;
using fullstack_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace fullstack_backend.Services
{
    public class MatchServices
    {
        private readonly DataContext _dataContext;

        public MatchServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<MatchModel>> GetAllMatches()
        {
            return await _dataContext.Matches.ToListAsync();
        }

        public async Task<MatchModel> GetMatchesById(int id)
        {
            return (await _dataContext.Matches.FindAsync(id))!;
        }

        public async Task<List<MatchModel>> GetMatchesByUserId(int id)
        {
            return await _dataContext.Matches.Where(match => match.UserId == id).ToListAsync();
        }

        public async Task<List<MatchModel>> GetMatchesByDate(string date)
        {
            return await _dataContext.Matches.Where(match => match.DaysAvailable == date).ToListAsync();
        }

        public async Task<bool> CreateMatch(MatchModel match)
        {
            await _dataContext.Matches.AddAsync(match);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> UpdateMatch(MatchModel match)
        {
            var matchToUpdate = await GetMatchesById(match.Id);

            if (matchToUpdate == null) return false;


            matchToUpdate.MyName = match.MyName;
            matchToUpdate.UserContent = match.UserContent;
            matchToUpdate.UserSport = match.UserSport;
            matchToUpdate.DaysAvailable = match.DaysAvailable;
            matchToUpdate.StartTime = match.StartTime;
            matchToUpdate.EndTime = match.EndTime;

            _dataContext.Matches.Update(matchToUpdate);
            return await _dataContext.SaveChangesAsync() != 0;
        }
    }
}