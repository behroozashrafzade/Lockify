using Core.Models;
using Lockify.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MyPasswordManager.Application.Services
{
    public class PasswordManager
    {
        private readonly string _localFilePath;
        private List<PasswordEntry> _passwords;

        public PasswordManager(string localFilePath)
        {
            _localFilePath = localFilePath;
            _passwords = LoadFromFile();
        }

        public IEnumerable<PasswordEntry> GetAll() => _passwords;

        public void Add(PasswordEntry entry)
        {
            entry.Id = Guid.NewGuid();
            entry.CreatedAt = DateTime.Now;
            entry.UpdatedAt = DateTime.Now;

            _passwords.Add(entry);
            SaveToFile();
        }

        public void Update(PasswordEntry entry)
        {
            var index = _passwords.FindIndex(p => p.Id == entry.Id);
            if (index >= 0)
            {
                entry.UpdatedAt = DateTime.Now;
                _passwords[index] = entry;
                SaveToFile();
            }
        }

        public void Delete(Guid id)
        {
            _passwords.RemoveAll(p => p.Id == id);
            SaveToFile();
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_passwords);
            File.WriteAllText(_localFilePath, json);
        }

        private List<PasswordEntry> LoadFromFile()
        {
            if (!File.Exists(_localFilePath))
                return new List<PasswordEntry>();

            var json = File.ReadAllText(_localFilePath);
            return JsonSerializer.Deserialize<List<PasswordEntry>>(json) ?? new List<PasswordEntry>();
        }
    }
}