import React from 'react'

interface DataTableProps<TData> {
  data: TData[]
  columns: any[] // TODO: Define proper column type
}

export function DataTable<TData>({ data, columns }: DataTableProps<TData>) {
  return (
    <div className="rounded-md border">
      {/* TODO: Implement Shadcn table here */}
      <table className="w-full caption-bottom text-sm">
        <thead className="[&_tr]:border-b">
          <tr className="hover:bg-muted/50 data-[state=selected]:bg-muted border-b transition-colors">
            {columns.map((column, index) => (
              <th
                key={index}
                className="h-12 px-4 text-left align-middle font-medium text-muted-foreground [&:has([role=checkbox])]:pr-0"
              >
                {column.header}
              </th>
            ))}
          </tr>
        </thead>
        <tbody className="[&_tr:last-child]:border-0">
          {data.map((row, rowIndex) => (
            <tr
              key={rowIndex}
              data-state={false}
              className="hover:bg-muted/50 data-[state=selected]:bg-muted border-b transition-colors"
            >
              {columns.map((column, colIndex) => (
                <td
                  key={colIndex}
                  className="p-4 align-middle [&:has([role=checkbox])]:pr-0"
                >
                  {row[column.accessorKey as keyof TData]}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}
