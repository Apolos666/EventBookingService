import React, { useRef, useState } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { Label } from "@/components/ui/label";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger, DialogFooter } from "@/components/ui/dialog";
import { PlusIcon, XIcon, ImageIcon } from "lucide-react";
import { EventDto, EventLocationDto, LocationDto } from '@/services/apis/events/uploadEvent';
import { ScrollArea } from '@/components/ui/scroll-area';

const UploadEventDialog: React.FC = () => {
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [locations, setLocations] = useState<EventLocationDto[]>([{
    location: {
      name: '',
      address: '',
      city: '',
      state: '',
      zipCode: '',
      country: '',
    },
    maxAttendees: 0,
    price: 0
  }]);
  const [imagePreview, setImagePreview] = useState<string | null>(null);
  const fileInputRef = useRef<HTMLInputElement>(null);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const eventData: EventDto = {
      name: formData.get('name') as string,
      description: formData.get('description') as string,
      startDateTime: new Date(formData.get('startDate') as string),
      endDateTime: new Date(formData.get('endDate') as string),
      eventLocationDtos: locations
    };
    console.log(eventData);
    setIsDialogOpen(false);
  };

  const addLocation = () => {
    setLocations([...locations, {
      location: {
        name: '',
        address: '',
        city: '',
        state: '',
        zipCode: '',
        country: '',
      },
      maxAttendees: 0,
      price: 0
    }]);
  };

  const removeLocation = (index: number) => {
    setLocations(locations.filter((_, i) => i !== index));
  };

  const updateLocation = (index: number, field: keyof EventLocationDto | keyof LocationDto, value: string | number) => {
    const newLocations = [...locations];
    if (field in newLocations[index].location) {
      (newLocations[index].location as any)[field] = value;
    } else {
      (newLocations[index] as any)[field] = value;
    }
    setLocations(newLocations);
  };

  const handleImageUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        setImagePreview(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  };

  const triggerFileInput = () => {
    fileInputRef.current?.click();
  };

  return (
    <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
      <DialogTrigger asChild>
        <Button variant="outline" className="hidden md:flex">
          <PlusIcon className="mr-2 h-4 w-4" />
          Add Event
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[700px] h-[90vh] flex flex-col p-0">
        <DialogHeader className="px-6 py-4 border-b">
          <DialogTitle>Add New Event</DialogTitle>
          <DialogDescription>
            Fill in the details of your new event. Click save when you're done.
          </DialogDescription>
        </DialogHeader>
        <ScrollArea className="flex-grow px-6 py-4">
          <form onSubmit={handleSubmit} className="space-y-4">
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="name" className="text-right">
                Event Name
              </Label>
              <Input id="name" name="name" className="col-span-3" required />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="description" className="text-right">
                Short Description
              </Label>
              <Textarea id="description" name="description" className="col-span-3" required />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="startDate" className="text-right">
                Start Date
              </Label>
              <Input id="startDate" name="startDate" type="datetime-local" className="col-span-3" required />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="endDate" className="text-right">
                End Date
              </Label>
              <Input id="endDate" name="endDate" type="datetime-local" className="col-span-3" required />
            </div>
            <div className="grid grid-cols-4 items-start gap-4">
              <Label className="text-right mt-2">
                Locations & Prices
              </Label>
              <div className="col-span-3 space-y-4">
                {locations.map((location, index) => (
                  <div key={index} className="space-y-2 p-4 border rounded-md">
                    <div className="flex justify-between items-center">
                      <h4 className="font-medium">Location {index + 1}</h4>
                      <Button type="button" variant="outline" size="icon" onClick={() => removeLocation(index)}>
                        <XIcon className="h-4 w-4" />
                      </Button>
                    </div>
                    <div className="grid grid-cols-2 gap-2">
                      <Input
                        placeholder="Name"
                        value={location.location.name}
                        onChange={(e) => updateLocation(index, 'name', e.target.value)}
                        required
                      />
                      <Input
                        placeholder="Address"
                        value={location.location.address}
                        onChange={(e) => updateLocation(index, 'address', e.target.value)}
                        required
                      />
                      <Input
                        placeholder="City"
                        value={location.location.city}
                        onChange={(e) => updateLocation(index, 'city', e.target.value)}
                        required
                      />
                      <Input
                        placeholder="State"
                        value={location.location.state}
                        onChange={(e) => updateLocation(index, 'state', e.target.value)}
                        required
                      />
                      <Input
                        placeholder="Zip Code"
                        value={location.location.zipCode}
                        onChange={(e) => updateLocation(index, 'zipCode', e.target.value)}
                        required
                      />
                      <Input
                        placeholder="Country"
                        value={location.location.country}
                        onChange={(e) => updateLocation(index, 'country', e.target.value)}
                        required
                      />
                      <Input
                        type="number"
                        placeholder="Max Attendees"
                        value={location.maxAttendees}
                        onChange={(e) => updateLocation(index, 'maxAttendees', parseInt(e.target.value))}
                        required
                      />
                      <Input
                        type="number"
                        placeholder="Price"
                        value={location.price}
                        onChange={(e) => updateLocation(index, 'price', parseFloat(e.target.value))}
                        required
                      />
                    </div>
                  </div>
                ))}
                <Button type="button" variant="outline" onClick={addLocation}>
                  Add Location
                </Button>
              </div>
            </div>
            <div className="grid grid-cols-4 items-start gap-4">
              <Label className="text-right mt-2">
                Event Image
              </Label>
              <div className="col-span-3">
                <Input
                  id="image"
                  name="image"
                  type="file"
                  accept="image/*"
                  className="hidden"
                  onChange={handleImageUpload}
                  ref={fileInputRef}
                />
                <div className="flex items-center space-x-2">
                  <Button type="button" onClick={triggerFileInput} variant="outline">
                    <ImageIcon className="mr-2 h-4 w-4" />
                    Upload Image
                  </Button>
                  {imagePreview && (
                    <div className="relative w-16 h-16">
                      <img
                        src={imagePreview}
                        alt="Event preview"
                        className="w-full h-full object-cover rounded"
                      />
                    </div>
                  )}
                </div>
              </div>
            </div>
          </form>
        </ScrollArea>
        <DialogFooter className="px-6 py-4 border-t">
          <Button type="submit" form="add-event-form">Save Event</Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export default UploadEventDialog;