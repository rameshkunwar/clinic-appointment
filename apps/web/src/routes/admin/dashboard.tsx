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
  author: string
  createdAt: string
  updatedAt: string
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
  {
    accessorKey: 'author',
    header: 'Author',
  },
  {
    accessorKey: 'createdAt',
    header: 'Created At',
    cell: (info) => new Date(info.row.original.createdAt).toLocaleString(),
  },
]

function AdminDashboard() {
  const [posts, setPosts] = useState<Post[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [newPostTitle, setNewPostTitle] = useState('')
  const [newPostContent, setNewPostContent] = useState('')
  const [isSubmitting, setIsSubmitting] = useState(false)

  useEffect(() => {
    const fetchPosts = async () => {
      try {
        const response = await fetch('/api/posts', { credentials: 'include' })
        if (!response.ok) {
          if (response.status === 401) {
            window.location.href = '/admin/login'
            return
          }
          throw new Error(`Error fetching posts: ${response.statusText}`)
        }
        const data = await response.json()
        setPosts(data.posts)
      } catch (err: any) {
        setError(err.message)
      } finally {
        setLoading(false)
      }
    }
    fetchPosts()
  }, [])

  const handleNewPostSubmit = async () => {
    if (!newPostTitle.trim() || !newPostContent.trim()) {
      alert('Please provide both a title and content.')
      return
    }

    setIsSubmitting(true)
    setError(null)
    try {
      const response = await fetch('/api/posts', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          title: newPostTitle,
          content: newPostContent,
          author: 'Admin',
        }),
      })

      if (!response.ok) {
        throw new Error(`Failed to create post: ${response.statusText}`)
      }

      const result = await response.json()

      // Update local state
      setPosts(prev => [
        {
          id: result.id,
          title: result.title,
          content: result.content,
          author: result.author,
          createdAt: result.createdAt,
          updatedAt: result.updatedAt,
        },
        ...prev,
      ])

      // Reset form
      setNewPostTitle('')
      setNewPostContent('')
      alert('Post created successfully!')
    } catch (err: any) {
      setError(err.message)
    } finally {
      setIsSubmitting(false)
    }
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
          <div className="space-y-4">
            <div>
              <label className="block text-sm font-medium mb-1">Title</label>
              <input
                type="text"
                className="w-full p-2 border rounded"
                placeholder="Enter post title"
                value={newPostTitle}
                onChange={(e) => setNewPostTitle(e.target.value)}
              />
            </div>
            <div>
              <label className="block text-sm font-medium mb-1">Content</label>
              <RichTextEditor
                content={newPostContent}
                onContentChange={setNewPostContent}
              />
            </div>
            <button
              onClick={handleNewPostSubmit}
              disabled={isSubmitting}
              className="mt-4 px-4 py-2 bg-blue-600 text-white rounded-md disabled:bg-gray-400"
            >
              {isSubmitting ? 'Saving...' : 'Save New Post'}
            </button>
          </div>
        </div>
      </div>
    </div>
  )
}
