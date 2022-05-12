using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTimeManage.MediatR.Commands.WorkItems;
public record DeleteWorkItemCommand(Guid PublicId) : IRequest<bool>;

