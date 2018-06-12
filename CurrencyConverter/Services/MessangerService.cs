using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyConverter.ExecutionControl;
using CurrencyConverter.Infrastructure;
using CurrencyConverter.Models;
using CurrencyConverter.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Services
{
    public class MessangerService : IMessangerService
    {
        private readonly LocalContext _context;

        public MessangerService(LocalContext context)
        {
            _context = context;
        }

        public async Task<ExecutionResult<Message>> GetMessageAsync(int id)
        {
            try
            {
                var message = await _context.Messages.Include(m => m.Replies).FirstOrDefaultAsync(m => m.Id == id);

                if (message == null)
                    throw new Exception("Message not found");

                return ExecutionResult<Message>.Success(message);
            }
            catch (Exception ex)
            {
                return new ExecutionResult<Message>(ExecutionResult.DomainFailedResult(new Dictionary<string, string>
                {
                    { "Bad Request", ex.Message }
                }));
            }
        }

        public async Task<ExecutionResult> PostMessageAsync(MessageDto dto)
        {
            try
            {
                var author = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.AuthorId);
                var message = Message.Factory.CreateNew(dto);

                if (author == null)
                    throw new Exception("Author not found");

                if (dto.ParentMessageId != null && dto.ParentMessageId != 0)
                {
                    var parentMessage = await _context.Messages.FirstOrDefaultAsync(m => m.Id == dto.ParentMessageId);

                    if (parentMessage == null)
                        throw new Exception("Message with given ParentMessageId not found");

                    parentMessage.Reply(message);
                }
                else
                    await _context.Messages.AddAsync(message);

                author.Posts.Add(message);
                await _context.SaveChangesAsync();

                return ExecutionResult.Success();
            }
            catch (Exception ex)
            {
                return ExecutionResult.DomainFailedResult(new Dictionary<string, string>
                {
                    { "Bad Request", ex.Message }
                });
            }
        }

        public async Task<ExecutionResult> EditMessageAsync(int id, MessageDto dto)
        {
            try
            {
                var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);

                if (message == null)
                    throw new Exception("Message not found");

                message.Update(dto);
                _context.Messages.Update(message);

                await _context.SaveChangesAsync();

                return ExecutionResult.Success();
            }
            catch (Exception ex)
            {
                return ExecutionResult.DomainFailedResult(new Dictionary<string, string>
                {
                    { "Bad Request", ex.Message }
                });
            }
        }

        public async Task<ExecutionResult> DeleteMessageAsync(int id)
        {
            try
            {
                var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);

                if (message == null)
                    throw new Exception("Message not found");

                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();

                return ExecutionResult.Success();
            }
            catch (Exception ex)
            {
                return ExecutionResult.DomainFailedResult(new Dictionary<string, string>
                {
                    { "Bad Request", ex.Message }
                });
            }
        }
    }
}