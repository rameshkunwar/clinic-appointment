# Role & Project Overview
You are an AI developer scaffolding and building a medical appointment system for a clinic.

# Architecture & Tech Stack
- Frontend: TanStack Start (React meta-framework), Vite, Tailwind CSS, Shadcn UI (TypeScript). Located in `apps/web`.
- Backend CMS: .NET 10 Web API, C#, Entity Framework Core, PostgreSQL (`clinic_cms_db`). Located in `apps/api`.
- Scheduling Engine: External Cal.diy container (`booking.clinic.com`) managed via Docker Compose (`cal_db`).
- Reverse Proxy: Caddy handling routing and automatic SSL/TLS on Hetzner.

# Workspace Rules
- Maintain a polyrepo-style folder structure: `apps/web`, `apps/api`, and `infra`.
- Never modify core Cal.diy source code; keep scheduling isolated as a container service.
- Use `dotnet build` for backend checks and `pnpm dev` (or `npm run dev`) for frontend verification.
- Complete one task fully before moving to the next. Do not scaffold multiple tasks at once.
- After each task, run the appropriate verification command and confirm it passes before stopping.

# Backend Architecture — Vertical Slice Architecture (VSA) + Wolverine
The backend follows Vertical Slice Architecture. Code is organized around features, not technical layers.

## Folder structure
Each feature lives entirely under `Features/` in its own subfolder:
Features/
└── Appointments/
├── AppointmentsController.cs
├── CreateAppointment/
│ ├── CreateAppointmentCommand.cs
│ ├── CreateAppointmentHandler.cs
│ └── CreateAppointmentResponse.cs
├── GetAppointment/
└── GetAppointments/



- One controller per feature group, thin — only routes and dispatches via `IMessageBus`.
- All request/response models, validation, and handler logic live inside the feature subfolder.
- Never place logic in the controller itself.
- Never create a shared `Services/` or `Repositories/` layer.

## Commands and handlers
- Commands are records: `public record CreateAppointmentCommand(...);`
- Handlers are static classes with a static `Handle` method:
```csharp
  public static class CreateAppointmentHandler
  {
      public static async Task<CreateAppointmentResponse> Handle(
          CreateAppointmentCommand command,
          AppDbContext db,
          CancellationToken ct)
      { ... }
  }
```
- Controllers dispatch via `IMessageBus.InvokeAsync<TResponse>()` — never call handlers directly.
- Do not use MediatR, no `IRequest<T>`, no `IRequestHandler<T>`.

## Validation
- Each command has a `[CommandName]Validator.cs` in the same feature subfolder.
- Validators inherit `AbstractValidator<TCommand>`.
- Never validate manually inside handlers — Wolverine calls the validator automatically before `Handle`.

## Wolverine registration
Register in `Program.cs` exactly as follows, before `builder.Build()`:

```csharp
builder.Host.UseWolverine(options =>
{
    options.UseFluentValidation();
    options.InvokeTracing = InvokeTracingMode.Full;
});

builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = true);
```

# Code Style — C# / .NET
- Single-exit return style: assign result to a named variable, return it at the end.
- No `switch (true)` chains; use data-driven lookup tables or pattern matching instead.
- English-only identifiers — no mixed-language naming.
- Follow EF Core conventions: `IHasTimestamps`, `moddatetime` triggers, `ValueGeneratedOnAddOrUpdate`.
- Use nullable reference types correctly: `required` for non-nullable navigation properties, `null!` only where EF requires it.

# Code Style — TypeScript / React
- English-only identifiers.
- Prefer explicit types over `any`.
- Use Shadcn UI primitives; do not roll custom components when a Shadcn equivalent exists.

# Proxy & Networking Rules
- The Caddy reverse proxy automatically passes the incoming `Host` header to the backend without modifications. 
- Caddy also automatically sets the `X-Forwarded-For`, `X-Forwarded-Proto`, and `X-Forwarded-Host` headers. The .NET API must be configured with `ForwardedHeadersOptions` to read these for proper JWT cookie validation and URL generation.
- For production reliability, utilize Caddy's active health checks by configuring the `health_uri` parameter to point to the .NET API's health endpoint.

# Frontend Build & Deployment (TanStack Start)
- When writing the Dockerfile for TanStack Start, you must use a multi-stage build. This keeps the final Docker image small because you do not include build tools like TypeScript or Vite in the final container.
- The TanStack Start Nitro build bundles its own dependencies directly into the `.output` directory, so the runtime stage does not need the `node_modules` folder or the `package.json` file.
- The production command to run the built TanStack application is `node .output/server/index.mjs`.

# Implementation Roadmap
Work through these tasks in order. Check off each task only after verification passes.

