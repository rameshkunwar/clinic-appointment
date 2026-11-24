import { createFileRoute } from '@tanstack/react-router'
import { useUser } from '../hooks/useUser'

export const Route = createFileRoute('/book')({
  component: Book,
})

function Book() {
  const { user, loading, error } = useUser()

  if (loading) {
    return <div className="p-2">Loading booking page...</div>
  }

  if (error) {
    return <div className="p-2 text-red-500">Error: {error}</div>
  }

  if (!user) {
    return <div className="p-2">Please log in to book an appointment.</div>
  }

  const bookingUrl = `https://booking.clinic.com?name=${encodeURIComponent(user.username)}&email=${encodeURIComponent(user.email)}&userId=${user.id}`

  return (
    <div className="p-2">
      <h3 className="text-xl font-bold mb-4">Book an Appointment</h3>
      <iframe
        src={bookingUrl}
        width="100%"
        height="800px"
        frameBorder="0"
      ></iframe>
    </div>
  )
}