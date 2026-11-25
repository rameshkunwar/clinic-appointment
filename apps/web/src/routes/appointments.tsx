import { createFile } from '@tanstack/react-start'
import { useQuery } from '@tanstack/react-query'

type Appointment = {
  appointmentId: string
  appointmentTime: string
  status: string
}

const columns = [
  { header: 'ID', accessorKey: 'appointmentId' },
  { header: 'Time', accessorKey: 'appointmentTime' },
  { header: 'Status', accessorKey: 'status' },
]

export default function AppointmentsPage() {
  const { data: appointments, isLoading, error } = useQuery({
    queryKey: ['appointments'],
    queryFn: async () => {
      const response = await fetch('/api/appointments')
      if (!response.ok) {
        throw new Error('Failed to fetch appointments')
      }
      return response.json()
    },
  })

  if (isLoading) return <div className="p-10">Loading appointments...</div>
  if (error) return <div className="p-10 text-red-500">Error loading appointments: {(error as Error).message}</div>

  return (
    <div className="container mx-auto py-10 px-4">
      <h1 className="text-2xl font-bold mb-6">Appointments</h1>
      <div className="border rounded-md">
        <table className="w-full text-sm">
          <thead className="bg-muted">
            <tr>
              {columns.map((column, index) => (
                <th key={index} className="p-4 text-left font-medium">{column.header}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {appointments?.map((appointment: Appointment, index: number) => (
              <tr key={index} className="border-t">
                {columns.map((column, colIndex) => (
                  <td key={colIndex} className="p-4">
                    {appointment[column.accessorKey as keyof Appointment]}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  )
}
