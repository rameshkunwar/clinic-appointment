import { useQuery } from '@tanstack/react-query';
import { api } from '../lib/api'; // I need to check if api is exported from lib/utils.ts

export function useAppointments() {
  return useQuery({
    queryKey: ['appointments'],
    queryFn: async () => {
      const response = await fetch('/api/appointments');
      if (!response.ok) {
        throw new Error('Failed to fetch appointments');
      }
      return response.json();
    },
  });
}
