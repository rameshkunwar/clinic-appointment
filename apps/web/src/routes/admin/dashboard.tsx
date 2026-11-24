import { createFileRoute } from '@tanstack/react-router'
import { useEffect, useState } from 'react'
import type { ColumnDef } from '@tanstack/react-table'
import { DataTable } from '../../components/data-table'
import RichTextEditor from '../../components/rich-text-editor'

export const Route = createFileRoute('/admin/dashboard')({
  component: AdminDashboard,
})

interface Post {
  id: string
  title: string
  content: string
}

const columns: ColumnDef<Post>[] = [
  {
    accessorKey: 'title',
    header: 'Title',
  },
  {
    accessorKey: 'content',
    header: 'Content',
  },
]

function AdminDashboard() {
  const [posts, setPosts] = useState<Post[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [newPostContent, setNewPostContent] = useState('')

  useEffect(() => {
    const fetchPosts = async () => {
      try {
        const response = await fetch('/api/posts', { credentials: 'include' })
        if (!response.ok) {
          if (response.status === 401) {
            window.location.href = '/admin/login' // Redirect to login if unauthorized
            return
          }
          throw new Error(`Error fetching posts: ${response.statusText}`)
        }
        const data = await response.json()
        setPosts(data)
      } catch (err: any) {
        setError(err.message)
      } finally {
        setLoading(false)
      }
    }
    fetchPosts()
  }, [])

  const handleNewPostSubmit = () => {
    console.log('New post content:', newPostContent)
    // TODO: Implement API call to save new post
  }

  if (loading) {
    return <div className="p-2">Loading dashboard...</div>
  }

  if (error) {
    return <div className="p-2 text-red-500">Error: {error}</div>
  }

  return (
    <div className="p-2">
      <h3 className="text-2xl font-bold mb-4">Admin Dashboard</h3>
      <div className="container mx-auto py-10">
        <h4 className="text-xl font-semibold mb-4">Manage Posts</h4>
        <DataTable columns={columns} data={posts} />

        <div className="mt-8">
          <h4 className="text-xl font-semibold mb-4">Create New Post</h4>
          <RichTextEditor
            content={newPostContent}
            onContentChange={setNewPostContent}
          />
          <button
            onClick={handleNewPostSubmit}
            className="mt-4 px-4 py-2 bg-blue-600 text-white rounded-md"
          >
            Save New Post
          </button>
        </div>
      </div>
    </div>
  )
}