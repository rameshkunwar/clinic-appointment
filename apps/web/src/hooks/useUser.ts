import { useState, useEffect } from 'react'

export interface UserProfile {
  id: string
  username: string
  email: string
}

export function useUser() {
  const [user, setUser] = useState<UserProfile | null>(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = await fetch('/api/Auth/me', { credentials: 'include' })
        if (!response.ok) {
          throw new Error(`Failed to fetch user: ${response.statusText}`)
        }
        const data = await response.json()
        setUser(data)
      } catch (err: any) {
        setError(err.message)
      } finally {
        setLoading(false)
      }
    }

    fetchUser()
  }, [])

  return { user, loading, error }
}