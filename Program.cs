using Hospital.Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hospital API", Version = "v1" });
});

// Use SQLite database (replace the file path as needed)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=hospital.db";
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hospital API v1");
    });
}

app.UseHttpsRedirection();

// CRUD endpoints for Appointment
app.MapGet("/appointments", async (HospitalDbContext db) => await db.Appointments.ToListAsync());
app.MapGet("/appointments/{id}", async (HospitalDbContext db, int id) =>
    await db.Appointments.FindAsync(id) is Appointment a ? Results.Ok(a) : Results.NotFound());
app.MapPost("/appointments", async (HospitalDbContext db, Appointment appointment) =>
{
    db.Appointments.Add(appointment);
    await db.SaveChangesAsync();
    return Results.Created($"/appointments/{appointment.Id}", appointment);
});
app.MapPut("/appointments/{id}", async (HospitalDbContext db, int id, Appointment updated) =>
{
    var appointment = await db.Appointments.FindAsync(id);
    if (appointment is null) return Results.NotFound();
    appointment.PatientId = updated.PatientId;
    appointment.DoctorId = updated.DoctorId;
    appointment.AppointmentDateTime = updated.AppointmentDateTime;
    appointment.IsEmergency = updated.IsEmergency;
    appointment.Notes = updated.Notes;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/appointments/{id}", async (HospitalDbContext db, int id) =>
{
    var appointment = await db.Appointments.FindAsync(id);
    if (appointment is null) return Results.NotFound();
    db.Appointments.Remove(appointment);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// CRUD endpoints for Doctor
app.MapGet("/doctors", async (HospitalDbContext db) => await db.Doctors.ToListAsync());
app.MapGet("/doctors/{id}", async (HospitalDbContext db, int id) =>
    await db.Doctors.FindAsync(id) is Doctor d ? Results.Ok(d) : Results.NotFound());
app.MapPost("/doctors", async (HospitalDbContext db, Doctor doctor) =>
{
    db.Doctors.Add(doctor);
    await db.SaveChangesAsync();
    return Results.Created($"/doctors/{doctor.Id}", doctor);
});
app.MapPut("/doctors/{id}", async (HospitalDbContext db, int id, Doctor updated) =>
{
    var doctor = await db.Doctors.FindAsync(id);
    if (doctor is null) return Results.NotFound();
    doctor.Name = updated.Name;
    doctor.Email = updated.Email;
    doctor.Phone = updated.Phone;
    doctor.Specialization = updated.Specialization;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/doctors/{id}", async (HospitalDbContext db, int id) =>
{
    var doctor = await db.Doctors.FindAsync(id);
    if (doctor is null) return Results.NotFound();
    db.Doctors.Remove(doctor);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// CRUD endpoints for EmergencyBooking
app.MapGet("/emergencybookings", async (HospitalDbContext db) => await db.EmergencyBookings.ToListAsync());
app.MapGet("/emergencybookings/{id}", async (HospitalDbContext db, int id) =>
    await db.EmergencyBookings.FindAsync(id) is EmergencyBooking e ? Results.Ok(e) : Results.NotFound());
app.MapPost("/emergencybookings", async (HospitalDbContext db, EmergencyBooking booking) =>
{
    db.EmergencyBookings.Add(booking);
    await db.SaveChangesAsync();
    return Results.Created($"/emergencybookings/{booking.Id}", booking);
});
app.MapPut("/emergencybookings/{id}", async (HospitalDbContext db, int id, EmergencyBooking updated) =>
{
    var booking = await db.EmergencyBookings.FindAsync(id);
    if (booking is null) return Results.NotFound();
    booking.PatientId = updated.PatientId;
    booking.ResponderId = updated.ResponderId;
    booking.Location = updated.Location;
    booking.AmbulanceLocation = updated.AmbulanceLocation;
    booking.Confirmed = updated.Confirmed;
    booking.Date = updated.Date;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/emergencybookings/{id}", async (HospitalDbContext db, int id) =>
{
    var booking = await db.EmergencyBookings.FindAsync(id);
    if (booking is null) return Results.NotFound();
    db.EmergencyBookings.Remove(booking);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// CRUD endpoints for EmergencyResponder
app.MapGet("/emergencyresponders", async (HospitalDbContext db) => await db.EmergencyResponders.ToListAsync());
app.MapGet("/emergencyresponders/{id}", async (HospitalDbContext db, int id) =>
    await db.EmergencyResponders.FindAsync(id) is EmergencyResponder e ? Results.Ok(e) : Results.NotFound());
app.MapPost("/emergencyresponders", async (HospitalDbContext db, EmergencyResponder responder) =>
{
    db.EmergencyResponders.Add(responder);
    await db.SaveChangesAsync();
    return Results.Created($"/emergencyresponders/{responder.Id}", responder);
});
app.MapPut("/emergencyresponders/{id}", async (HospitalDbContext db, int id, EmergencyResponder updated) =>
{
    var responder = await db.EmergencyResponders.FindAsync(id);
    if (responder is null) return Results.NotFound();
    responder.Name = updated.Name;
    responder.Email = updated.Email;
    responder.Phone = updated.Phone;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/emergencyresponders/{id}", async (HospitalDbContext db, int id) =>
{
    var responder = await db.EmergencyResponders.FindAsync(id);
    if (responder is null) return Results.NotFound();
    db.EmergencyResponders.Remove(responder);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// CRUD endpoints for Nurse
app.MapGet("/nurses", async (HospitalDbContext db) => await db.Nurses.ToListAsync());
app.MapGet("/nurses/{id}", async (HospitalDbContext db, int id) =>
    await db.Nurses.FindAsync(id) is Nurse n ? Results.Ok(n) : Results.NotFound());
app.MapPost("/nurses", async (HospitalDbContext db, Nurse nurse) =>
{
    db.Nurses.Add(nurse);
    await db.SaveChangesAsync();
    return Results.Created($"/nurses/{nurse.Id}", nurse);
});
app.MapPut("/nurses/{id}", async (HospitalDbContext db, int id, Nurse updated) =>
{
    var nurse = await db.Nurses.FindAsync(id);
    if (nurse is null) return Results.NotFound();
    nurse.Name = updated.Name;
    nurse.Email = updated.Email;
    nurse.Phone = updated.Phone;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/nurses/{id}", async (HospitalDbContext db, int id) =>
{
    var nurse = await db.Nurses.FindAsync(id);
    if (nurse is null) return Results.NotFound();
    db.Nurses.Remove(nurse);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// CRUD endpoints for Patient
app.MapGet("/patients", async (HospitalDbContext db) => await db.Patients.ToListAsync());
app.MapGet("/patients/{id}", async (HospitalDbContext db, int id) =>
    await db.Patients.FindAsync(id) is Patient p ? Results.Ok(p) : Results.NotFound());
app.MapPost("/patients", async (HospitalDbContext db, Patient patient) =>
{
    db.Patients.Add(patient);
    await db.SaveChangesAsync();
    return Results.Created($"/patients/{patient.Id}", patient);
});
app.MapPut("/patients/{id}", async (HospitalDbContext db, int id, Patient updated) =>
{
    var patient = await db.Patients.FindAsync(id);
    if (patient is null) return Results.NotFound();
    patient.Name = updated.Name;
    patient.Email = updated.Email;
    patient.Phone = updated.Phone;
    patient.MedicalHistory = updated.MedicalHistory;
    patient.Password = updated.Password;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/patients/{id}", async (HospitalDbContext db, int id) =>
{
    var patient = await db.Patients.FindAsync(id);
    if (patient is null) return Results.NotFound();
    db.Patients.Remove(patient);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// CRUD endpoints for Report
app.MapGet("/reports", async (HospitalDbContext db) => await db.Reports.ToListAsync());
app.MapGet("/reports/{id}", async (HospitalDbContext db, int id) =>
    await db.Reports.FindAsync(id) is Report r ? Results.Ok(r) : Results.NotFound());
app.MapPost("/reports", async (HospitalDbContext db, Report report) =>
{
    db.Reports.Add(report);
    await db.SaveChangesAsync();
    return Results.Created($"/reports/{report.Id}", report);
});
app.MapPut("/reports/{id}", async (HospitalDbContext db, int id, Report updated) =>
{
    var report = await db.Reports.FindAsync(id);
    if (report is null) return Results.NotFound();
    report.PatientId = updated.PatientId;
    report.DoctorId = updated.DoctorId;
    report.ReportDetails = updated.ReportDetails;
    report.CreatedOn = updated.CreatedOn;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/reports/{id}", async (HospitalDbContext db, int id) =>
{
    var report = await db.Reports.FindAsync(id);
    if (report is null) return Results.NotFound();
    db.Reports.Remove(report);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// CRUD endpoints for Schedule
app.MapGet("/schedules", async (HospitalDbContext db) => await db.Schedules.ToListAsync());
app.MapGet("/schedules/{id}", async (HospitalDbContext db, int id) =>
    await db.Schedules.FindAsync(id) is Schedule s ? Results.Ok(s) : Results.NotFound());
app.MapPost("/schedules", async (HospitalDbContext db, Schedule schedule) =>
{
    db.Schedules.Add(schedule);
    await db.SaveChangesAsync();
    return Results.Created($"/schedules/{schedule.Id}", schedule);
});
app.MapPut("/schedules/{id}", async (HospitalDbContext db, int id, Schedule updated) =>
{
    var schedule = await db.Schedules.FindAsync(id);
    if (schedule is null) return Results.NotFound();
    schedule.DoctorId = updated.DoctorId;
    schedule.AvailableFrom = updated.AvailableFrom;
    schedule.AvailableTo = updated.AvailableTo;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/schedules/{id}", async (HospitalDbContext db, int id) =>
{
    var schedule = await db.Schedules.FindAsync(id);
    if (schedule is null) return Results.NotFound();
    db.Schedules.Remove(schedule);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// CRUD endpoints for Tracking
app.MapGet("/trackings", async (HospitalDbContext db) => await db.Trackings.ToListAsync());
app.MapGet("/trackings/{id}", async (HospitalDbContext db, int id) =>
    await db.Trackings.FindAsync(id) is Tracking t ? Results.Ok(t) : Results.NotFound());
app.MapPost("/trackings", async (HospitalDbContext db, Tracking tracking) =>
{
    db.Trackings.Add(tracking);
    await db.SaveChangesAsync();
    return Results.Created($"/trackings/{tracking.Id}", tracking);
});
app.MapPut("/trackings/{id}", async (HospitalDbContext db, int id, Tracking updated) =>
{
    var tracking = await db.Trackings.FindAsync(id);
    if (tracking is null) return Results.NotFound();
    tracking.PatientId = updated.PatientId;
    tracking.AmbulanceLocation = updated.AmbulanceLocation;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/trackings/{id}", async (HospitalDbContext db, int id) =>
{
    var tracking = await db.Trackings.FindAsync(id);
    if (tracking is null) return Results.NotFound();
    db.Trackings.Remove(tracking);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Login endpoint for Patient
app.MapPost("/login", async (HospitalDbContext db, LoginRequest login) =>
{
    var patient = await db.Patients
        .FirstOrDefaultAsync(p => p.Email == login.Email && p.Password == login.Password);

    if (patient is null)
        return Results.Unauthorized();

    return Results.Ok(new
    {
        patient.Id,
        patient.Name,
        patient.Email
    });
});

app.Run();

// Define the DbContext
public class HospitalDbContext : DbContext
{
    public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<EmergencyBooking> EmergencyBookings { get; set; }
    public DbSet<EmergencyResponder> EmergencyResponders { get; set; }
    public DbSet<Nurse> Nurses { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Tracking> Trackings { get; set; }
}

public record LoginRequest(string Email, string Password);
