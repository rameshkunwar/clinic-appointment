import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/book')({
  component: Book,
})

function Book() {
  return (
    <div className="p-2">
      <h3>Book an Appointment</h3>
      <iframe
        src="https://booking.clinic.com"
        width="100%"
        height="800px"
        frameBorder="0"
      ></iframe>
    </div>
  )
}
