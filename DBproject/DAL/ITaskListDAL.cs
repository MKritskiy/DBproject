﻿using DBproject.DAL.Models;

namespace DBproject.DAL
{
    public interface ITaskListDAL
    {
        Task<TaskListModel> GetTaskList(int id);
        Task<IEnumerable<TaskListModel>> GetTaskListsByTeamId(int teamid);
        Task<IEnumerable<TaskListModel>> GetTaskLists();
        Task<int> CreateTaskList(TaskListModel model);
        Task UpdateTaskList(TaskListModel model);
    }
}
