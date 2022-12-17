﻿using API.Models.Tennis;
using System.Collections.Generic;

namespace API.Repositories.Tennis
{
    public interface ICourtRepository
    {
        int SaveCourt(Court court);
        void SaveRating(NewRating rating);
        void AddCourtToCourtConditionTable(int courtRecnum, int ratingTotal, int numberOfRatings);
    }
}