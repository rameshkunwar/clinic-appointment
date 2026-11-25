import { createFileRoute } from '@tanstack/react-router'
import { useUser } from '../hooks/useUser'
import { useAppForm } from '../hooks/demo.form'
import { TextField } from '../components/demo.FormComponents'
import { TextArea } from '../components/demo.FormComponents'
import { SubscribeButton } from '../components/demo.FormComponents'
import { useNavigate } from '@tanstack/react-router'

export const Route = createFileRoute('/book')({
  component: Book,
})

function Book() {
  const { user, loading, error } = useUser()
  const navigate = useNavigate()

  if (loading) {
    return <div className="p-2">Loading booking page...</div>
  }

  if (error) {
    return <div className="p-2 text-red-500">Error: {error}</div>
  }

  if (!user) {
    return <div className="p-2">Please log in to book an appointment.</div>
  }

  const form = useAppForm({
    defaultValues: {
      appointmentTime: new Date().toISOString().slice(0, 16),
      description: '',
    },
    onSubmit: async ({ value }) => {
      try {
        const response = await fetch('/api/appointments/create', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            patientId: user.id,
            appointmentTime: value.appointmentTime,
            description: value.description,
          }),
        })

        if (!response.ok) {
          const errData = await response.text()
          throw new Error(`Failed to book appointment: ${errData}`)
        }

        navigate({ to: '/appointments' })
      } catch (err) {
        console.error(err)
        alert(`Error: ${err.message}`)
      }
    },
  })

  return (
    <div className="p-8">
      <h3 className="text-xl font-bold mb-4">Book an Appointment</h3>
      <form
        onSubmit={(e) => {
          e.preventDefault()
          form.handleSubmit()
        }}
        className="space-y-4"
      >
        <div className="space-y-2">
          <TextField label="Appointment Time" placeholder="YYYY-MM-DDTHH:MM" />
        </div>
        <div className="space-y-2">
          <TextArea label="Description" rows={4} />
        </div>
        <SubscribeButton label="Confirm Booking" />
      </form>

      <div className="mt-12 pt-6 border-t">
        <h4 className="text-sm font-semibold mb-2">External Booking Engine</h4>
        <p className="text-sm text-muted-foreground mb-4">
          Alternatively, you can use our external booking system:
        </p>
        <iframe
          src={`https://booking.clinic.com?name=${encodeURIComponent(user.username)}&email=${encodeURIComponent(user.email)}&userId=${user.id}`}
          width="100%"
          height="600px"
          frameBorder="0"
        ></iframe>
      </div>
    </div>
  )
}
